using GameOfLife.CSharp.Engine.Extensions;
using Silent.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.CSharp.Engine
{
    public class Universe : IUniverse
    {
        private readonly IGridGraph<Cell> _cellsGraph = GridGraph<Cell>.FromSize(5, 5);
        private readonly Dictionary<Guid, Offset> _cellOffsets = new();

        private Universe()
        {
            Identity = Guid.NewGuid();
            Size = Size.None;
            Worlds = new List<IImmutableGrid>();
        }

        private Universe(Guid identity, Size size, IGridGraph<Cell> cellsGraph, Dictionary<Guid, Offset> cellOffsets)
        {
            Identity = identity;
            Size = size;
            _cellsGraph = cellsGraph;
            _cellOffsets = cellOffsets;
        }

        public static IUniverse FromPattern(PopulationPattern pattern)
        {
            if (pattern is null) throw new ArgumentNullException(nameof(pattern));

            var cellOffsets = new Dictionary<Guid, Offset>();
            var cellsGraph = GridGraph<Cell>.Empty();

            for (int row = 0; row < pattern.Height; row++)
            {
                for (int column = 0; column < pattern.Width; column++)
                {
                    var position = new Position(row, column);
                    var cell = pattern[row, column] ? Cell.Alive : Cell.Dead;
                    var offset = new Offset(column, row);
                    cellOffsets[cell.Identity] = offset;
                    cellsGraph.SetVertex(position, cell);
                }
            }

            // TODO: calculate the size
            Size size = Size.None;
            return new Universe(Guid.NewGuid(), size, cellsGraph, cellOffsets);
        }

        #region Public Properties

        public static IUniverse Empty => new Universe();

        public Guid Identity { get; }

        public Cell this[int row, int column] => InternalFindAndGet(row, column) ?? Cell.Empty;

        public Size Size { get; }

        public ICollection<IImmutableGrid> Worlds { get; }

        public IReadOnlyCollection<Cell> Cells => _cellsGraph.Vertices.Select(x => x.Value).ToList();

        #endregion

        #region Public Methods

        public IUniverse Evolve()
        {
            var gameOfLifeRules = new SquareCellPopulationRules();
            // TODO: clone the graph, so that it possile to modify new instance without changing the existing one
            //var graphCopy = _blueGraph.Clone();
            var graphCopy = GridGraph<Cell>.Empty();

            foreach (var existingCell in Cells)
            {
                var vertexOffset = _cellOffsets[existingCell.Identity];
                var vertexCell = _cellsGraph[vertexOffset.Top, vertexOffset.Left];
                var vertexPosition = new Position(vertexOffset.Top, vertexOffset.Left);
                var aliveNeighbours = vertexCell.Neighbors.Count(vertex => vertex.Value.IsAlive());
                var populationNextState = gameOfLifeRules.GetNextPopulationState(existingCell, aliveNeighbours);
                var updatedCell = existingCell with { Population = populationNextState };
                graphCopy.SetVertex(vertexPosition, updatedCell);
            }

            return new Universe(Identity, Size, graphCopy, _cellOffsets);
        }

        public IUniverse Merge(IUniverse universe, Offset offset)
        {
            if (universe is not null)
            {
                int left = Math.Min(0, _cellOffsets.Min(x => x.Value.Left));
                int top = Math.Min(0, _cellOffsets.Min(x => x.Value.Top));
                int right = Math.Min(0, _cellOffsets.Max(x => x.Value.Left));
                int bottom = Math.Min(0, _cellOffsets.Max(x => x.Value.Top));

                var shift = new Offset(Math.Abs(left), Math.Abs(top));
                var size = new Size(right - left, bottom - top);

                return new Universe(Identity, size, _cellsGraph, _cellOffsets);
            }

            return null;
        }

        public IUniverse Join(Cell first, Cell second)
        {
            var sourceOffset = _cellOffsets[first.Identity];
            var sourceVertex = _cellsGraph[sourceOffset.Top, sourceOffset.Left];
            var sourcePosition = new Position(sourceOffset.Top, sourceOffset.Left);
            var targetOffset = _cellOffsets[second.Identity];
            var targetVertex = _cellsGraph[targetOffset.Top, targetOffset.Left];
            var targetPosition = new Position(targetOffset.Top, targetOffset.Left);
            _cellsGraph.SetEdge(sourcePosition, targetPosition, 1);
            _cellsGraph.SetEdge(targetPosition, sourcePosition, 1);
            return this;
        }

        public IUniverse Split(Cell first, Cell second)
        {
            var sourceOffset = _cellOffsets[first.Identity];
            var sourceVertex = _cellsGraph[sourceOffset.Top, sourceOffset.Left];
            var sourcePosition = new Position(sourceOffset.Top, sourceOffset.Left);
            var targetOffset = _cellOffsets[second.Identity];
            var targetVertex = _cellsGraph[targetOffset.Top, targetOffset.Left];
            var targetPosition = new Position(targetOffset.Top, targetOffset.Left);
            _cellsGraph.RemoveEdge(sourcePosition, targetPosition);
            _cellsGraph.RemoveEdge(targetPosition, sourcePosition);
            return this;
        }

        #endregion

        private Cell? InternalFindAndGet(int row, int column) => _cellsGraph[row, column]?.Value;
    }
}

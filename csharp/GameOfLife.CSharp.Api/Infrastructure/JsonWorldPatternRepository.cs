using GameOfLife.CSharp.Api.Extensions;
using GameOfLife.CSharp.Api.Models;
using GameOfLife.Engine;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace GameOfLife.CSharp.Api.Infrastructure
{
    public class JsonWorldPatternRepository : IWorldPatternRepository
    {
        private readonly List<PopulationPattern> _patterns = new List<PopulationPattern>();
        private readonly IOptions<GameOptions> _options;
        private volatile int _identity = 0;

        public JsonWorldPatternRepository(IOptions<GameOptions> options)
        {
            _options = options;
        }

        public PopulationPattern SavePattern(PopulationPattern pattern)
        {
            string filePath = GetSanitizedFilePath(pattern.Name);
            if (!File.Exists(filePath))
            {
                pattern.PatternId = Interlocked.Increment(ref _identity);
                _patterns.Add(pattern);
            }

            var vm = pattern.ToPatternViewVM();
            vm.Name = pattern.Name;
            vm.PatternId = pattern.PatternId;
            SerializePattern(vm);

            return pattern;
        }

        public PopulationPattern GetPatternById(int patternId)
        {
            return _patterns.FirstOrDefault(x => x.PatternId == patternId);
        }

        public ICollection<PopulationPattern> GetUserPatterns(int userId)
        {
            if (_patterns.Count == 0)
            {
                var patterns = Directory.EnumerateFiles(_options.Value.PatternsDirectory, "*.json")
                    .Select(DeserializePattern)
                    .Select(ToPopulationPattern)
                    .ToList();
                _patterns.AddRange(patterns);
                _identity = _patterns.Max(x => x.PatternId);
            }

            return _patterns;
        }

        private PopulationPattern ToPopulationPattern(PopulationPatternViewVM vm)
        {
            var pattern = new PopulationPattern(vm.PatternId, vm.Name, vm.Width, vm.Height);

            foreach (var row in vm.Rows)
            {
                foreach (var column in row.Columns)
                {
                    pattern.TrySetCellState(column.Row, column.Column, column.IsAlive);
                }
            }

            return pattern;
        }

        private string GetSanitizedFilePath(string patternName)
        {
            char[] invalids = Path.GetInvalidFileNameChars();
            string sanitizedName = invalids.Aggregate(patternName.ToLower(), (current, symbol) => current.Replace(symbol, '-'));
            string filePath = Path.Combine(_options.Value.PatternsDirectory, $"{sanitizedName}.json");
            return filePath;
        }

        private void SerializePattern(PopulationPatternViewVM vm)
        {
            string filePath = GetSanitizedFilePath(vm.Name);
            string json = JsonConvert.SerializeObject(vm);
            File.WriteAllText(filePath, json);
        }

        private PopulationPatternViewVM DeserializePattern(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<PopulationPatternViewVM>(json);
        }
    }
}

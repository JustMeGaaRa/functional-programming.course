using GameOfLife.CSharp.Api.Extensions;
using GameOfLife.CSharp.Api.ViewModels;
using GameOfLife.CSharp.Engine;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameOfLife.CSharp.Api.Infrastructure
{
    public class JsonWorldPatternRepository : IWorldPatternRepository
    {
        private readonly Dictionary<int, PopulationPattern> _patterns = new Dictionary<int, PopulationPattern>();
        private readonly IOptions<GameOptions> _options;

        public JsonWorldPatternRepository(IOptions<GameOptions> options)
        {
            _options = options;
        }

        public PopulationPattern? SavePattern(PopulationPattern pattern)
        {
            return SerializePattern(pattern)
                ? _patterns[pattern.PatternId] = pattern
                : null;
        }

        public PopulationPattern? GetPatternById(int patternId)
        {
            return _patterns.ContainsKey(patternId)
                ? _patterns[patternId]
                : null;
        }

        public ICollection<PopulationPattern> GetUserPatterns(int userId)
        {
            if (_patterns.Count == 0)
            {
                var patterns = Directory.EnumerateFiles(_options.Value.PatternsDirectory, "*.json")
                    .Select(DeserializePattern)
                    .Select(ToPopulationPattern)
                    .Select(pattern => _patterns[pattern.PatternId] = pattern)
                    .ToList();
                PopulationPatternIdentity.RestoreIdentity(_patterns.Values.Max(x => x.PatternId));
            }

            return _patterns.Values;
        }

        private PopulationPattern ToPopulationPattern(PopulationPatternViewVM vm)
        {
            var pattern = PopulationPattern.FromSize(vm.PatternId, vm.Name, vm.Width, vm.Height);

            foreach (var row in vm.Rows ?? new List<PopulationPatternRowVM>())
            {
                foreach (var column in row.Columns ?? new List<PopulationPatternCellVM>())
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
            return Path.Combine(_options.Value.PatternsDirectory, $"{sanitizedName}.json");
        }

        private bool SerializePattern(PopulationPattern pattern)
        {
            if (pattern != null)
            {
                var vm = pattern.ToPatternViewVM();
                string filePath = GetSanitizedFilePath(vm.Name);
                string json = JsonConvert.SerializeObject(vm);
                File.WriteAllText(filePath, json);
                return true;
            }

            return false;
        }

        private PopulationPatternViewVM DeserializePattern(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<PopulationPatternViewVM>(json);
            }

            return null;
        }
    }
}

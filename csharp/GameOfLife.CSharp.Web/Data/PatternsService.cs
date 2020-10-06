using GameOfLife.CSharp.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Data
{
    public class PatternsService : IPatternsService
    {
        private readonly HttpClient _httpClient;

        public PatternsService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44370");
        }

        public async Task<UserInfo> CreateUser()
        {
            var url = $"api/users";

            var response = await _httpClient.PostAsync(url, null);
            var content = await response.Content.ReadFromJsonAsync<UserInfo>();

            return content;
        }

        public async Task<ICollection<PatternInfo>> GetPattersByUserId(int userId)
        {
            var url = $"api/users/{userId}/patterns";

            var response = await _httpClient.GetFromJsonAsync<ICollection<PatternInfo>>(url);

            return response;
        }

        public async Task<PatternInfo> CreatePattern(int userId, PatternInfo pattern)
        {
            var url = $"api/users/{userId}/patterns";

            var response = await _httpClient.PostAsJsonAsync(url, pattern);
            var content = await response.Content.ReadFromJsonAsync<PatternInfo>();

            return content;
        }

        public async Task<World> GetPatternView(int userId, int patternId)
        {
            var url = $"api/users/{userId}/patterns/{patternId}/view";

            var response = await _httpClient.GetFromJsonAsync<World>(url);

            return response;
        }

        public async Task<World> GetPatternCell(int userId, int patternId, WorldCell column)
        {
            var url = $"api/users/{userId}/patterns/{patternId}/view/cell";

            var response = await _httpClient.PutAsJsonAsync(url, column);
            var content = await response.Content.ReadFromJsonAsync<World>();

            return content;
        }
    }
}

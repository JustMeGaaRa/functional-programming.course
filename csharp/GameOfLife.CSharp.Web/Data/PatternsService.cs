using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Data
{
    public class PatternsService : IPatternsService
    {
        private readonly HttpClient _httpClient;

        public PatternsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44370");
        }

        public async Task<ICollection<Pattern>> GetPattersByUserId(int userId)
        {
            var url = $"api/users/{userId}/patterns";

            var response = await _httpClient.GetJsonAsync<ICollection<Pattern>>(url);

            return response;
        }

        public async Task<Pattern> CreatePattern(int userId, Pattern pattern)
        {
            var url = $"api/users/{userId}/patterns";

            var response = await _httpClient.PostJsonAsync<Pattern>(url, pattern);

            return response;
        }

        public async Task<World> GetPatternView(int userId, int patternId)
        {
            var url = $"api/users/{userId}/patterns/{patternId}/view";

            var response = await _httpClient.GetJsonAsync<World>(url);

            return response;
        }

        public async Task<World> GetPatternCell(int userId, int patternId, WorldColumn column)
        {
            var url = $"api/users/{userId}/patterns/{patternId}/view/cell";

            var response = await _httpClient.PutJsonAsync<World>(url, column);

            return response;
        }

        public async Task<UserInfo> CreateUser()
        {
            var url = $"api/users";

            var response = await _httpClient.PostJsonAsync<UserInfo>(url, null);

            return response;
        }
    }
}

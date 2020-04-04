using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Data
{
    public class GameService : IGameService
    {
        private readonly HubConnection _hubConnection;

        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;

        public GameService()
        {
            _hubConnection = new HubConnectionBuilder()
             .WithUrl("https://localhost:44370/game")
             .Build();
        }

        public async Task Connect()
        {
            await _hubConnection.StartAsync();
        }

        public async Task End(int userId)
        {
            await _hubConnection.SendAsync("EndUserGame", userId);
        }

        public async Task Start(int userId, int patternId)
        {
            await _hubConnection.SendAsync("StartGameFromPattern", userId, patternId);
        }

        public void Subscribe(Action<World> action)
        {
            _hubConnection.On("UpdateGameWorld", action);
        }
    }
}

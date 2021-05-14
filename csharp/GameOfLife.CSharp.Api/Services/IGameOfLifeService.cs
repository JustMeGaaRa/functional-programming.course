using GameOfLife.CSharp.Engine;
using System;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Api.Services
{
    public interface IGameOfLifeService
    {
        Generation CreateFromPattern(int userId, int patternId);

        Task<bool> StartGameAsync(int userId, Guid instanceId);

        Task<bool> StopGameAsync(int userId, Guid instanceId);

        Task<bool> RemoveGameAsync(int userId, Guid instanceId);
    }
}

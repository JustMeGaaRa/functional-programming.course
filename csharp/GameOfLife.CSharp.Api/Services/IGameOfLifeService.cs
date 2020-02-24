using GameOfLife.CSharp.Engine;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Api.Services
{
    public interface IGameOfLifeService
    {
        Task<Generation> StartGameAsync(int userId, int patternId);

        Task EndGameAsync(int userId);
    }
}

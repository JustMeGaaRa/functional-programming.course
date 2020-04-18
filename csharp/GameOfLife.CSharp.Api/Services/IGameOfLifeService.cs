using GameOfLife.CSharp.Engine;
using System;

namespace GameOfLife.CSharp.Api.Services
{
    public interface IGameOfLifeService
    {
        Generation CreateFromPattern(int userId, int patternId);

        bool StartGame(int userId, Guid instanceId);

        bool EndGame(int userId, Guid instanceId);

        bool MergeGames(int userId, Guid firstId, Guid secondId);
    }
}

using GameOfLife.CSharp.Web.Models;
using System;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Data
{
    public interface IGameService
    {
        Task Connect();
        void Subscribe(Action<World> action);
        Task Start(int userId, int patternId);
        Task End(int userId);
    }
}
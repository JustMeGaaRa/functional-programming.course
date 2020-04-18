using GameOfLife.CSharp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Data
{
    public interface IPatternsService
    {
        Task<UserInfo> CreateUser();

        Task<Pattern> CreatePattern(int userId, Pattern pattern);

        Task<ICollection<Pattern>> GetPattersByUserId(int userId);

        Task<World> GetPatternView(int userId, int patternId);

        Task<World> GetPatternCell(int userId, int patternId, WorldColumn column);
    }
}
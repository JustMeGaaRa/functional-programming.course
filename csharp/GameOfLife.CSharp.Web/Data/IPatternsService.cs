using GameOfLife.CSharp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Data
{
    public interface IPatternsService
    {
        Task<ICollection<Pattern>> GetPattersByUserId(int userId);
    }
}
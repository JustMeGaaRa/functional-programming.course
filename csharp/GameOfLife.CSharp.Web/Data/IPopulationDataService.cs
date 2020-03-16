using GameOfLife.CSharp.Web.Models;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Data
{
    public interface IPopulationDataService
    {
        Task<World> GetPopulationData();
    }
}
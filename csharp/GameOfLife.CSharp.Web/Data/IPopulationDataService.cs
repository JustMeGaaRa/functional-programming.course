using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Data
{
    public interface IPopulationDataService
    {
        Task<IList<List<string>>> GetPopulationData();
    }
}
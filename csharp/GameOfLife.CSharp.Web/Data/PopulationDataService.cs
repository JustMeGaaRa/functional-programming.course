using GameOfLife.CSharp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Data
{
    public class PopulationDataService : IPopulationDataService
    {
        public async Task<World> GetPopulationData()
        {
            var rows = new List<WorldRow>
            {
                new WorldRow
                {
                    Number = 1,
                    Columns = new List<WorldColumn>
                    {
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 1,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 2,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 3,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 4,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 5,
                            IsAlive = true
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 6,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 7,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 8,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 9,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 10,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 11,
                            IsAlive = true
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 12,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 13,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 14,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 1,
                            Column = 15,
                            IsAlive = false
                        }
                    }
                },
                new WorldRow
                {
                    Number = 2,
                    Columns = new List<WorldColumn>
                    {
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 1,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 2,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 3,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 4,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 5,
                            IsAlive = true
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 6,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 7,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 8,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 9,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 10,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 11,
                            IsAlive = true
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 12,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 13,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 14,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 2,
                            Column = 15,
                            IsAlive = false
                        }
                    }
                },
                new WorldRow
                {
                    Number = 3,
                    Columns = new List<WorldColumn>
                    {
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 1,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 2,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 3,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 4,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 5,
                            IsAlive = true
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 6,
                            IsAlive = true
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 7,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 8,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 9,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 10,
                            IsAlive = true
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 11,
                            IsAlive = true
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 12,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 13,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 14,
                            IsAlive = false
                        },
                        new WorldColumn
                        {
                            Row = 3,
                            Column = 15,
                            IsAlive = false
                        }
                    }
                }
            };

            var world = new World
            {
                Rows = rows
            };

            return await Task.FromResult(world);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Archero.UnitSystem;

namespace Archero.Logic
{
    public class UnitFactory : IUnitFactory
    {
        public Unit CreateUnit(string id, IReadOnlyCollection<UnitStat> stats)
        {
            return new Unit(id, stats.Select(i=>(i.Id, i.Value)).ToList());
        }
    }
}

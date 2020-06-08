using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Archero.UnitSystem
{
    // todo: подумать над сериализацией
    public class Unit
    {
        public Unit(string id, IReadOnlyCollection<(string statId, object value)> stats)
        {
            Id = id;
            foreach (var stat in stats)
            {
                this.stats.Add(stat.statId, stat.value);
            }
        }

        public bool SetStatValue(string statId, object value)
        {
            if (stats.ContainsKey(statId))
            {
                stats[statId] = value;
                return true;
            }

            return false;
        }

        public object GetStatValue(string statId)
        {
            return stats[statId];
        }

        public string Id { get; }

        #region vats
        
        private Dictionary<string, object> stats = new Dictionary<string, object>();

        #endregion   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archero.UnitSystem
{
    [System.Serializable]
    public class UnitStat
    {
        public UnitStat(string id, object value)
        {
            this.id = id;
            this.value = value;
        }

        public string Id => id;
        public object Value => value;
        
        #region vars

        [SerializeField] private string id;
        [SerializeField] private object value;

        #endregion
    }
}
using System;
using UnityEngine;

namespace Utils.Editor
{
    public class EditorPosRenderer : MonoBehaviour
    {
        protected void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, radius);
        }
        
        #region vars

        [SerializeField] private Color color;
        [SerializeField] private float radius = 1.0f;

        #endregion
    }
}

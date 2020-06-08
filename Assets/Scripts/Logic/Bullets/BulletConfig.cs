using UnityEngine;

namespace Archero.Logic
{
    [CreateAssetMenu(menuName = "Create bullet config")]
    public class BulletConfig : ScriptableObject
    {
        public string Id;
        public GameObject Prefab;
    }
}

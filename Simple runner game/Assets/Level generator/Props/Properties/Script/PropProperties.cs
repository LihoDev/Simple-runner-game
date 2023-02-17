using UnityEngine;

namespace Prop
{
    [CreateAssetMenu(fileName = "New PropProperties", menuName = "Obstacles/PropProperties", order = 1)]
    public class PropProperties : ScriptableObject
    {
        public Transform Prefab { get => _prefab; private set { _prefab = value; } }
        [SerializeField] private Transform _prefab;
    }
}
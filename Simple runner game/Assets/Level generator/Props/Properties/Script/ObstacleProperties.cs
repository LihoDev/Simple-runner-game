using UnityEngine;

namespace Prop
{
    [CreateAssetMenu(fileName = "New ObstacleProperties", menuName = "Obstacles/ObstacleProperties", order = 2)]
    public class ObstacleProperties : PropProperties
    {
        public int Length { get => _length; private set { _length = value; } }
        [SerializeField, Min(1)] private int _length = 1;
    }
}
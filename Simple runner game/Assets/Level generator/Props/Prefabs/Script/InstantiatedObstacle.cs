using UnityEngine;

namespace Prop
{
    public class InstantiatedObstacle : InstantiatedProp
    {
        public ObstacleProperties Properties { get => _properties; private set { _properties = value; } }
        [SerializeField] private ObstacleProperties _properties;
    }
}
using Prop;
using UnityEngine;

namespace Player
{
    public class HealthCollector : Collector
    {
        [SerializeField] private CollisionDetector _collisionDetector;

        protected override void Collect(Collider collider)
        {
            if (collider.TryGetComponent(out HealthGem healthGem))
            {
                _collisionDetector.RestoreHealth();
                collider.gameObject.SetActive(false);
                base.Collect(collider);
            }
        }
    }
}
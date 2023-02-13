using Prop;
using UnityEngine;

namespace Player
{
    public class HealthCollector : MonoBehaviour
    {
        [SerializeField] private CollisionDetector _collisionDetector;

        private void OnTriggerEnter(Collider collider)
        {
            CollectHealth(collider);
        }

        private void CollectHealth(Collider collider)
        {
            if (collider.TryGetComponent(out HealthGem healthGem))
            {
                _collisionDetector.RestoreHealth();
                collider.gameObject.SetActive(false);
            }
        }
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Collector : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnCollect;

        private void OnTriggerEnter(Collider collider)
        {
            Collect(collider);
        }

        protected virtual void Collect(Collider collider)
        {
            OnCollect?.Invoke();
        }
    }
}
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Thrower : MonoBehaviour
    {
        [SerializeField] private float _force = 1;
        private BoxCollider _boxCollider;
        private Rigidbody _rigidbody;

        public void ThrowObject()
        {
            transform.parent = null;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(Random.insideUnitSphere * _force, ForceMode.Impulse);
            _boxCollider.enabled = true;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
        }
    }
}
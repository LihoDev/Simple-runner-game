using System.Collections;
using UnityEngine;

namespace Player
{
    public class ForwardMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 2;

        private void Update()
        {
            transform.Translate(_speed * Time.deltaTime * transform.forward);
        }
    }
}

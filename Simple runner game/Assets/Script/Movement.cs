using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _speed = 2;

        private void Update()
        {
            transform.Translate(transform.forward * _speed * Time.deltaTime);
        }
    }
}

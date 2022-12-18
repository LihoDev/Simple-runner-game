using System.Collections;
using UnityEngine;

namespace Player
{
    public class ForwardMovement : MonoBehaviour
    {
        [SerializeField] private float _startSpeed = 10f;
        [SerializeField] private float acceleration = 1.01f;

        private void Update()
        {
            transform.Translate(_startSpeed * Time.deltaTime * transform.forward);
            _startSpeed += acceleration;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AnimationCaller))]
    public class DrinkCoffe : MonoBehaviour
    {
        [SerializeField] private AnimationCaller _animationCaller;
        [SerializeField, Min(0)] private float _delay;
        private float _time;

        private void Start()
        {
            _time = _delay;
        }

        private void FixedUpdate()
        {
            if (_time > 0)
                _time -= Time.fixedDeltaTime;
            else
            {
                _time = _delay;
                _animationCaller.CallDrink();
            }
        }
    }
}
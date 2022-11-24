using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class CurvedWorld : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] private AnimationCurve _curvatureX;
        [SerializeField, Min(0)] private float _speed;
        [SerializeField] private string _curveXProperty = "_CurveX";

        private float _currentTime;

        private void FixedUpdate()
        {
            _currentTime += _speed * Time.fixedDeltaTime;
            if (_currentTime > 1)
                _currentTime = 0;
            _material.SetFloat(_curveXProperty, _curvatureX.Evaluate(_currentTime));
        }
    }
}
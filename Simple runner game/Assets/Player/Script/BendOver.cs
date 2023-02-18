using System.Collections;
using UnityEngine;

namespace Player
{
    public class BendOver : MonoBehaviour
    {
        public bool Idle { get; private set; } = true;
        [SerializeField, Min(0)] private float _bendOverTime;
        [SerializeField] private Vector3 _bendOverCollisionSize = Vector3.one;
        [SerializeField] private Coroutine _active;
        [SerializeField] private AnimationCaller _animationCaller;

        public void StartBendOver()
        {
            if (_active != null)
                StopCoroutine(_active);
            _active = StartCoroutine(BendOverTimer());
            _animationCaller.CallBendOverStart();
        }

        public void StopBendOver()
        {
            StopCoroutine(_active);
            _active = null;
            transform.localScale = Vector3.one;
            Idle = true;
            _animationCaller.CallBendOverStop();
        }

        private IEnumerator BendOverTimer()
        {
            Idle = false;
            float time = 0;
            transform.localScale = _bendOverCollisionSize;
            while (time < _bendOverTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            StopBendOver();
        }
    }
}
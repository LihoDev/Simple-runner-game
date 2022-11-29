using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class JumpMovement : MonoBehaviour
    {
        [SerializeField] private float _moveDownSpeed = 0;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField, Min(0)] private float _jumpSpeed;
        [SerializeField, Min(0)] private float _jumpHeight;

        private Coroutine _jump;

        public void Jump()
        {
            if (_jump != null)
                return;
            _jump = StartCoroutine(MoveJumpCurve());
        }

        public void MoveDown()
        {
            if (_jump != null)
                StopCoroutine(_jump);
            _jump = StartCoroutine(MoveToTarget(Vector3.zero));
        }

        private IEnumerator MoveToTarget(Vector3 target)
        {
            while (Vector3.Distance(transform.localPosition, target) > 0.001f)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, _moveDownSpeed * Time.deltaTime);
                yield return null;
            }
            _jump = null;
        }

        private IEnumerator MoveJumpCurve()
        {
            float time = 0;
            while (time < 1)
            {
                time += _jumpSpeed * Time.deltaTime;
                transform.localPosition = new Vector3(transform.localPosition.x, _jumpHeight * _jumpCurve.Evaluate(time), transform.localPosition.z);
                yield return null;
            }
            _jump = null;
        }
    }
}
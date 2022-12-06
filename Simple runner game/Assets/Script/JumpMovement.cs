using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class JumpMovement : MonoBehaviour
    {
        [SerializeField] private float _moveDownSpeed = 0;
        [SerializeField] private AnimationCurve _upCurve;
        [SerializeField] private AnimationCurve _downCurve;
        [SerializeField, Min(0)] private float _jumpSpeed;
        [SerializeField, Min(0)] private float _jumpHeight;
        [SerializeField, Min(0)] private Vector3 _rayIndent;

        private Coroutine _moving;

        public void Jump()
        {
            if (_moving == null)
                _moving = StartCoroutine(MoveJumpCurve());
        }

        public void Down()
        {
            if (_moving != null)
            {
                StopCoroutine(_moving);
                _moving = StartCoroutine(MoveDown(_moveDownSpeed));
            }
        }

        private IEnumerator MoveUp(float startHeight)
        {
            float time = 0;
            while (time < 1f)
            {
                time += _jumpSpeed * Time.deltaTime;
                transform.localPosition = new Vector3(0, startHeight + _jumpHeight * _upCurve.Evaluate(time), 0);
                yield return null;
            }
        }

        private IEnumerator MoveDown(float speed)
        {
            float time = 0;
            float startHeight = transform.position.y;
            float previousHieght = GetGroundHieght();
            while (previousHieght < transform.localPosition.y)
            {
                previousHieght = GetGroundHieght();
                time += speed * Time.deltaTime;
                float newHieght = startHeight * _downCurve.Evaluate(time);
                transform.localPosition = new Vector3(0, (newHieght < previousHieght) ? previousHieght : newHieght, 0);
                yield return null;
            }
            _moving = null;
        }

        private IEnumerator MoveJumpCurve()
        {
            float startHeight = transform.localPosition.y;
            yield return MoveUp(startHeight);
            yield return MoveDown(_jumpSpeed);
        }

        private float GetDistanceGround()
        {
            if (Physics.Raycast(transform.position + _rayIndent, -transform.up, out RaycastHit hit) && hit.collider != null)
                return Vector3.Distance(transform.position, hit.point) * ((hit.point.y > transform.position.y) ? -1 : 1);
            return 0;
        }

        private float GetGroundHieght()
        {
            if (Physics.Raycast(transform.position + _rayIndent, -transform.up, out RaycastHit hit) && hit.collider != null)
                return hit.point.y;
            return 0;
        }

        private void SetHeight()
        {
            if (Physics.Raycast(transform.position + _rayIndent, -transform.up, out RaycastHit hit) && hit.collider != null)
                gameObject.transform.position = hit.point;
        }

        private void Update()
        {
            if (_moving == null)
            {
                if (GetDistanceGround() > 0.1f && transform.localPosition.y > 0)
                    _moving = StartCoroutine(MoveDown(_jumpSpeed));
                else
                    SetHeight();
            }
        }
    }
}
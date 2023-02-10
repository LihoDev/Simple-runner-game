using System.Collections;
using UnityEngine;

namespace Player
{
    public class JumpMovement : MonoBehaviour
    {
        public bool Idle { get; private set; } = true;
        [SerializeField] private float _moveDownSpeed = 0;
        [SerializeField] private AnimationCurve _upCurve;
        [SerializeField] private AnimationCurve _downCurve;
        [SerializeField, Min(0)] private float _jumpSpeed;
        [SerializeField, Min(0)] private float _jumpHeight;
        [SerializeField, Min(0)] private Vector3 _rayIndent;
        [SerializeField] private LayerMask _ignoreLayer;
        [SerializeField] private Transform _camera;
        [SerializeField, Min(0)] private float _cameraMovingSpeed = 5;
        [SerializeField] private AnimationCaller _animationCaller;
        private Coroutine _moving;

        public void Jump()
        {
            if (_moving == null)
            {
                _animationCaller.ResetStopAction();
                _moving = StartCoroutine(MoveJumpCurve());
                _animationCaller.CallJump();
            }
        }

        public void Down()
        {
            if (_moving != null)
            {
                StopCoroutine(_moving);
                _animationCaller.CallDown();
                _moving = StartCoroutine(MoveDown(_moveDownSpeed));
            }
        }

        public void StopMoving()
        {
            StopAllCoroutines();
            Idle = true;
        }

        private IEnumerator MoveUp(float startHeight)
        {
            Idle = false;
            float time = 0;
            while (time < 1f)
            {
                time += _jumpSpeed * Time.deltaTime;
                if (GetGroundHieght() > transform.localPosition.y)
                    break;
                transform.localPosition = new Vector3(0, startHeight + _jumpHeight * _upCurve.Evaluate(time), 0);
                yield return null;
            }
        }

        private IEnumerator MoveDown(float speed)
        {
            Idle = false;
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
            _animationCaller.CallStopAction();
            _moving = null;
            Idle = true;
        }

        private IEnumerator MoveJumpCurve()
        {
            float startHeight = transform.localPosition.y;
            yield return MoveUp(startHeight);
            yield return MoveDown(_jumpSpeed);
        }

        private float GetDistanceGround()
        {
            if (RaycastDown(out RaycastHit hit))
                return Vector3.Distance(transform.position, hit.point) * ((hit.point.y > transform.position.y) ? -1 : 1);
            return 0;
        }

        private float GetGroundHieght()
        {
            if (RaycastDown(out RaycastHit hit))
                return hit.point.y;
            return 0;
        }

        private void SetHeight()
        {
            if (RaycastDown(out RaycastHit hit))
            {
                gameObject.transform.position = hit.point;
                _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, new Vector3(0, hit.point.y, 0), _cameraMovingSpeed * Time.deltaTime);
            }
        }

        private bool RaycastDown(out RaycastHit hit)
        {
            return Physics.Raycast(new Ray(transform.position + _rayIndent, -transform.up), out hit, 1000f, ~_ignoreLayer) && hit.collider != null;
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
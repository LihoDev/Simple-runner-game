using System.Collections;
using UnityEngine;

namespace Player
{
    public class SideMovement : MonoBehaviour
    {
        public int CurrentRoad { get => _currentRoad; }

        [SerializeField] private float _speed = 0;
        [SerializeField, Min(1)] private int _sideJumpDistance = 1;
        [SerializeField, Min(1)] private int _countRoad;
        [SerializeField] private float _cameraOffset = 1;
        [SerializeField] private Transform _camera;

        private int _currentRoad = 2;
        private int _previousRoad = 2;
        private Coroutine _transformMovement;
        private Coroutine _cameraMovement;

        public void MoveRight()
        {
            //if (_currentRoad + 1 <= (_countRoad - 1) / 2 + 1)
            if (_currentRoad + 1 < _countRoad)
            {
                _previousRoad = _currentRoad;
                _currentRoad++;
                StartMovement();
            }

        }

        public void MoveLeft()
        {
            //if (_currentRoad - 1 >= -((_countRoad - 1) / 2 + 1))
            if (_currentRoad - 1 >= 0)
            {
                _previousRoad = _currentRoad;
                _currentRoad--;
                StartMovement();
            }
        }

        public void StopMoving()
        {
            StopAllCoroutines();
        }

        public void AbortMoving()
        {
            _currentRoad = _previousRoad;
            StartMovement();
        }

        private void StartMovement()
        {
            if (_transformMovement != null)
                StopCoroutine(_transformMovement);
            if (_cameraMovement != null)
                StopCoroutine(_cameraMovement);
            float roadIndexValue = RoadIndexValue(_currentRoad);
            _transformMovement = StartCoroutine(MoveToTarget(transform, new Vector3(roadIndexValue * _sideJumpDistance, 0, 0)));
            _cameraMovement = StartCoroutine(MoveToTarget(_camera, new Vector3(roadIndexValue * _sideJumpDistance - (_sideJumpDistance / 2) * roadIndexValue / _cameraOffset, _camera.localPosition.y, _camera.localPosition.z)));
        }

        private float RoadIndexValue(int index)
        {
            return index - (_countRoad - 1) / 2;
        }

        private IEnumerator MoveToTarget(Transform moveObject, Vector3 target)
        {
            while (Vector3.Distance(moveObject.localPosition, target) > 0.001f)
            {
                moveObject.localPosition = Vector3.MoveTowards(moveObject.localPosition, target, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
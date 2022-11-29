using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class SegmentInstancer : MonoBehaviour
    {
        [SerializeField, Min(2)] private int _lenght;
        [SerializeField] private Transform _prefab;
        [SerializeField, Min(1)] private int _maxCountOnScene;
        [SerializeField] private Transform _player;

        private List<Transform> _instances = new List<Transform>();
        private int _fierstSegmentIndex = 0;
        private int _lastSegmentPosition = 0;
        private int _oldPlayerDistance = 0;

        private void Start()
        {
            InstantiatePrefabs();
            if (_player == null)
            {
                Debug.LogError("Player missing!");
                return;
            }
            _oldPlayerDistance = _lastSegmentPosition - (_maxCountOnScene * _lenght) + _lenght;
        }

        private void InstantiatePrefabs()
        {
            if (_prefab == null)
            {
                Debug.LogError("Prefab missing!");
                return;
            }
            _lastSegmentPosition -= _lenght;
            for (var i = 0; i < _maxCountOnScene; i++)
            {
                _instances.Add(Instantiate(_prefab, new Vector3(0, 0, _lastSegmentPosition), Quaternion.identity));
                _lastSegmentPosition += _lenght;
            }
        }

        private void FixedUpdate()
        {
            if (_player.position.z - _oldPlayerDistance > _lenght)
            {
                MoveLastSegment();
                _oldPlayerDistance = _lastSegmentPosition - (_maxCountOnScene * _lenght) + _lenght;
            }
        }

        private void MoveLastSegment()
        {
            _instances[_fierstSegmentIndex].position = new Vector3(0, 0, _lastSegmentPosition);
            _lastSegmentPosition += _lenght;
            _fierstSegmentIndex++;
            if (_fierstSegmentIndex > _instances.Count - 1)
                _fierstSegmentIndex = 0;
        }

    }
}

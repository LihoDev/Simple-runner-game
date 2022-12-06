using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class PropInstancer : MonoBehaviour
    {
        [SerializeField] protected Transform _prefab;
        [SerializeField, Min(2)] protected int _lenght;
        [SerializeField, Min(1)] protected int _maxCountOnScene;
        [SerializeField] protected Transform _player;

        protected List<Transform> _instances = new List<Transform>();
        protected int _fierstSegmentIndex = 0;
        protected int _lastSegmentPosition = 0;
        protected int _oldPlayerDistance = 0;

        protected virtual void Start()
        {
            InstantiatePrefabs();
            if (_player == null)
            {
                Debug.LogError("Player missing!");
                return;
            }
            _oldPlayerDistance = _lastSegmentPosition - (_maxCountOnScene * _lenght) + _lenght;
        }

        protected virtual void InstantiatePrefabs()
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

        protected virtual void FixedUpdate()
        {
            if (_player.position.z - _oldPlayerDistance > _lenght)
            {
                MoveLastSegment();
                _oldPlayerDistance = _lastSegmentPosition - (_maxCountOnScene * _lenght) + _lenght;
            }
        }

        protected virtual void MoveLastSegment()
        {
            _instances[_fierstSegmentIndex].position = new Vector3(0, 0, _lastSegmentPosition);
            _lastSegmentPosition += _lenght;
            _fierstSegmentIndex++;
            if (_fierstSegmentIndex > _instances.Count - 1)
                _fierstSegmentIndex = 0;
        }
    }
}
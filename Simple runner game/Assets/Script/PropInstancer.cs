using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class PropInstancer : MonoBehaviour
    {
        [SerializeField, Min(1)] protected int _lenght;
        [SerializeField, Min(0)] private int _fierstIndent = 0;
        [SerializeField] protected Transform _player;
        protected List<Transform> _instances = new List<Transform>();
        protected int _fierstSegmentIndex = 0;
        protected int _lastSegmentPosition = 0;
        protected int _oldPlayerDistance = 0;

        [SerializeField] private List<Transform> _prefabs = new List<Transform>();
        [SerializeField, Min(1)] private int _countFront;
        [SerializeField, Min(1)] private int _countRear;
        private int MaxCountOnScene { get => _countFront + _countRear; }

        protected virtual void Start()
        {
            InstantiatePrefabs();
            if (_player == null)
            {
                Debug.LogError("Player missing!");
                return;
            }
            _oldPlayerDistance = _fierstIndent + (_lenght * _countRear);
            //_oldPlayerDistance = _lastSegmentPosition - (MaxCountOnScene * _lenght) + _lenght;
        }

        protected virtual void InstantiatePrefabs()
        {
            if (_prefabs.Count == 0)
            {
                Debug.LogError("Prefabs missing!");
                return;
            }
            _lastSegmentPosition = _fierstIndent;
            //_lastSegmentPosition -= _lenght;
            int prefabIndex = 0;
            for (var i = 0; i < MaxCountOnScene; i++)
            {
                _instances.Add(Instantiate(_prefabs[prefabIndex], new Vector3(0, 0, _lastSegmentPosition), Quaternion.identity));
                _lastSegmentPosition += _lenght;
                Show();
                MoveSegmentIndex();
                prefabIndex++;
                if (prefabIndex > _prefabs.Count - 1)
                    prefabIndex = 0;
            }
        }

        protected virtual void FixedUpdate()
        {
            if (_player.position.z - _oldPlayerDistance > _lenght)
            {
                PlaceProps();
                Show();
                _oldPlayerDistance = _lastSegmentPosition - (_countFront * _lenght) + _lenght;
                MoveSegmentIndex();
            }
        }

        protected virtual void Show()
        {

        }

        protected virtual void PlaceProps()
        {
            _instances[_fierstSegmentIndex].position = new Vector3(0, 0, _lastSegmentPosition);
            _lastSegmentPosition += _lenght;
        }

        private void MoveSegmentIndex()
        {
            _fierstSegmentIndex++;
            if (_fierstSegmentIndex == _instances.Count)
                _fierstSegmentIndex = 0;
        }
    }
}
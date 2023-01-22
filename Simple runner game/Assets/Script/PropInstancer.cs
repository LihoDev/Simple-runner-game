using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class PropInstancer : MonoBehaviour
    {
        [SerializeField, Min(1)] protected float _length;
        [SerializeField] protected SideMovement _player;
        protected List<Transform> _instances = new List<Transform>();
        protected int _fierstSegmentIndex = 0;
        protected float _lastSegmentPosition = 0;
        protected float _oldPlayerDistance = 0;

        [SerializeField, Min(0)] private float _fierstIndent = 0;
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
            _oldPlayerDistance = _fierstIndent + (_length * _countRear);
        }

        protected virtual void InstantiatePrefabs()
        {
            if (_prefabs.Count == 0)
            {
                Debug.LogError("Prefabs missing!");
                return;
            }
            _lastSegmentPosition = _fierstIndent;
            int prefabIndex = 0;
            for (var i = 0; i < MaxCountOnScene; i++)
            {
                _instances.Add(Instantiate(_prefabs[prefabIndex], new Vector3(0, 0, _lastSegmentPosition), Quaternion.identity));
                PlaceProps();
                //_lastSegmentPosition += _length;
                Show();
                MoveSegmentIndex();
                prefabIndex++;
                if (prefabIndex > _prefabs.Count - 1)
                    prefabIndex = 0;
            }
        }

        protected virtual void FixedUpdate()
        {
            if (_player.transform.position.z - _oldPlayerDistance > _length)
            {
                PlaceProps();
                _oldPlayerDistance = _lastSegmentPosition - (_countFront * _length) + _length;
                Show();
                MoveSegmentIndex();
            }
        }

        protected virtual void Show() { }

        protected virtual void PlaceProps()
        {
            _instances[_fierstSegmentIndex].position = new Vector3(0, 0, _lastSegmentPosition);
            _lastSegmentPosition += _length;
        }

        private void MoveSegmentIndex()
        {
            _fierstSegmentIndex++;
            if (_fierstSegmentIndex == _instances.Count)
                _fierstSegmentIndex = 0;
        }
    }
}
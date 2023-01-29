using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class ObjectReplacer : MonoBehaviour
    {
        [SerializeField, Min(1)] protected float _length;
        [SerializeField] protected SideMovement _player;
        [SerializeField, Min(1)] protected int _countFront;
        [SerializeField, Min(1)] protected int _countRear;
        [SerializeField, Min(1)] protected int _maxCount;
        protected List<Transform> _rows = new List<Transform>();
        protected int _rowIndex = 0;
        protected float _lastSegmentPosition = 0;
        protected float _oldPlayerDistance = 0;
        protected int MaxCountActive { get => _countFront + _countRear; }

        [SerializeField] private float _fierstIndent = 0;
        [SerializeField] private List<Transform> _prefabs = new List<Transform>();

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
            if (_maxCount * _prefabs.Count < MaxCountActive)
            {
                Debug.LogError("Max count * count prefabs is less than count front + count rear");
                return;
            }
            foreach (Transform prefab in _prefabs)
                for (var i = 0; i < _maxCount; i++)
                {
                    Transform instance = Instantiate(prefab, new Vector3(0, 0, _lastSegmentPosition), Quaternion.identity);
                    _rows.Add(instance);
                    instance.gameObject.SetActive(false);
                }
                    
            for (var i = 0; i < MaxCountActive; i++)
            {
                PlaceProps();
                Show();
                MoveRowIndex();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (_player.transform.position.z - _oldPlayerDistance > _length)
            {
                PlaceProps();
                _oldPlayerDistance = _lastSegmentPosition - (_countFront * _length) + _length;
                Show();
                MoveRowIndex();
            }
        }

        protected virtual void Show() { }

        protected virtual void PlaceProps()
        {
            _rows[_rowIndex].gameObject.SetActive(true);
            _rows[_rowIndex].position = new Vector3(0, 0, _lastSegmentPosition);
            _lastSegmentPosition += _length;
        }

        protected virtual void MoveRowIndex()
        {
            _rowIndex++;
            if (_rowIndex == _rows.Count)
                _rowIndex = 0;
        }
    }
}
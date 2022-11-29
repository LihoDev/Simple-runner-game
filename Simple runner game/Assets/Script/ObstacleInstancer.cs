using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class ObstacleInstancer : MonoBehaviour
    {
        [SerializeField] private Transform _prefab;
        [SerializeField, Min(1)] private int _maxCountOnScene;
        [SerializeField] private int _indent;
        [SerializeField] private int _firstIndent;
        [SerializeField] private Transform _player;

        private List<Transform> _instances = new List<Transform>();

        private void Start()
        {
            for (var i = 0; i < _maxCountOnScene; i++)
            {
                _instances.Add(Instantiate(_prefab, transform.position, Quaternion.identity));
            }
        }
    }
}
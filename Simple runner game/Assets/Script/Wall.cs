using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private List<Transform> _aislesPrefabs;
        [SerializeField] private List<Transform> _deadEndsPrefabs;
        [SerializeField] private List<Transform> _carsPrefabs;
        [SerializeField] private Transform _rampPrefab;
        [SerializeField, Min(0)] private float _roadWidth = 4;
        [SerializeField, Min(1)] private int _countRoad = 3;
        [SerializeField, Min(0)] private int _maxTrainLength = 1;
        [SerializeField, Min(1)] private int _carLength = 1;

        private List<Transform> _hiddenRamps = new List<Transform>();
        private List<List<Transform>> _hiddenCars = new List<List<Transform>>();
        private List<List<Transform>> _hiddenAisles = new List<List<Transform>>();
        private List<List<Transform>> _hiddenDeadEnds = new List<List<Transform>>();
        private List<Transform> _active = new List<Transform>();

        [ContextMenu("Randomize")]
        public void RandomizeWall()
        {
            HideWall();
            for (var road = 0; road < _countRoad; road++)
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        PlaceObstacle(Random.Range(0, 2) < 1 ? _hiddenDeadEnds[road][Random.Range(0, _deadEndsPrefabs.Count)] : _hiddenAisles[road][Random.Range(0, _aislesPrefabs.Count)], road, 0);
                        break;
                    case 1:
                        PlaceTrain(road);
                        break;
                }
            }
            int guaranteedPassage = Random.Range(0, _countRoad);
            HideObstacleOnRoad(guaranteedPassage);
            PlaceObstacle(_hiddenAisles[guaranteedPassage][Random.Range(0, _aislesPrefabs.Count)], guaranteedPassage, 0);
        }

        public void HideWall()
        {
            foreach (Transform obstacle in _active)
            {
                obstacle.gameObject.SetActive(false);
            }
            _active.Clear();
        }

        private void PlaceTrain(int roadIndex)
        {
            int indent = 0;
            if (Random.Range(0, 2) > 0)
            {
                PlaceObstacle(_hiddenRamps[roadIndex], roadIndex, 0);
                indent += _carLength;
            }
            for (var i=0; i<Random.Range(0, _maxTrainLength); i++)
            {
                PlaceObstacle(_hiddenCars[roadIndex][i], roadIndex, indent);
                indent += _carLength;
            }
        }

        private void HideObstacleOnRoad(int roadIndex)
        {
            for (var i = 0; i < _aislesPrefabs.Count; i++)
            {
                _hiddenAisles[roadIndex][i].gameObject.SetActive(false);
                _active.Remove(_hiddenAisles[roadIndex][i]);
            }   
            for (var i = 0; i < _deadEndsPrefabs.Count; i++)
            {
                _hiddenDeadEnds[roadIndex][i].gameObject.SetActive(false);
                _active.Remove(_hiddenDeadEnds[roadIndex][i]);
            }
            for (var i = 0; i < _maxTrainLength; i++)
            {
                _hiddenCars[roadIndex][i].gameObject.SetActive(false);
                _active.Remove(_hiddenCars[roadIndex][i]);
            }
            _hiddenRamps[roadIndex].gameObject.SetActive(false);
            _active.Remove(_hiddenRamps[roadIndex]);
        }

        private void PlaceObstacle(Transform obstacle, int roadIndex, int indent)
        {
            _active.Add(obstacle);
            obstacle.gameObject.SetActive(true);
            obstacle.localPosition = new Vector3((roadIndex - (_countRoad - 1) / 2) * _roadWidth, 0, indent);
        }

        private void InstantiatePrefabs()
        {
            if (_aislesPrefabs.Count == 0)
            {
                Debug.LogError("Aisles prefabs missing");
                return;
            }
            if (_deadEndsPrefabs.Count == 0)
            {
                Debug.LogError("DeadEnds prefabs missing");
                return;
            }
            if (_rampPrefab == null && _maxTrainLength > 0)
            {
                Debug.LogError("Ramp prefab missing");
                return;
            }
            if (_carsPrefabs.Count == 0 && _maxTrainLength > 0)
            {
                Debug.LogError("Cats prefabs missing");
                return;
            }
            for (var road = 0; road < _countRoad; road++)
            {
                List<Transform> aisles = new List<Transform>();
                List<Transform> deadEnds = new List<Transform>();
                List<Transform> cars = new List<Transform>();
                _hiddenAisles.Add(aisles);
                _hiddenDeadEnds.Add(deadEnds);
                _hiddenCars.Add(cars);
                foreach (Transform prefab in _aislesPrefabs)
                    aisles.Add(InstantiateObstacle(prefab));
                foreach (Transform prefab in _deadEndsPrefabs)
                    deadEnds.Add(InstantiateObstacle(prefab));
                for (var i = 0; i < _maxTrainLength; i++)
                    cars.Add(InstantiateObstacle(_carsPrefabs[Random.Range(0, _carsPrefabs.Count)]));
                _hiddenRamps.Add(InstantiateObstacle(_rampPrefab));
            }
        }

        private Transform InstantiateObstacle(Transform prefab)
        {
            Transform instance = Instantiate(prefab, transform.position, Quaternion.identity);
            instance.parent = transform;
            instance.gameObject.SetActive(false);
            return instance;
        }

        private void Start()
        {
            InstantiatePrefabs();
            RandomizeWall();
        }
    }
}
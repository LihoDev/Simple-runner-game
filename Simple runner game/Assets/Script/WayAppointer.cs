using Obstacles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class WayAppointer : PropInstancer
    {
        [SerializeField] private List<ObstacleProperties> _platformPrefabs = new List<ObstacleProperties>();
        [SerializeField, Min(1)] private int _maxCountPlatform = 40;
        [SerializeField] private List<ObstacleProperties> _obstaclePrefabs = new List<ObstacleProperties>();
        [SerializeField, Min(1)] private int _maxCountObstacle = 40;
        [SerializeField] private List<ObstacleProperties> _rampPrefabs = new List<ObstacleProperties>();
        [SerializeField, Min(1)] private int _maxCountRamp = 3;
        [SerializeField, Range(0, 100)] private float _changeWayFrequency;
        [SerializeField, Range(0, 100)] private float _rampFrequency;
        [SerializeField, Range(0, 100)] private float _obstacleFrequency;
        [SerializeField, Min(0)] private float _obstacleMinDistance;
        [SerializeField, Min(1)] private int _minWayLength;
        [SerializeField, Min(1)] private float _minTurnLength;
        [Space]
        [SerializeField, Min(0)] private float _roadWidth = 4;
        [SerializeField, Min(1)] private int _countRoad = 3;

        private List<Platform> _platformInstances = new List<Platform>();
        private List<Obstacle> _obstacleInstances = new List<Obstacle>();
        private List<Ramp> _rampsInstances = new List<Ramp>();
        private List<Coin> _coinsInstances = new List<Coin>();
        private List<float> _lastObstaclesPosition = new List<float>();
        private int _currentWayLength = 0;
        private int _currentWay = 1;
        private int _currentWaySecond = 1;
        private bool _turn;

        protected override void Show()
        {
            if (_instances[_fierstSegmentIndex].TryGetComponent(out Row row))
            {
                RebuildRow(row);
            }
            base.Show();
        }

        protected override void PlaceProps()
        {
            _currentWayLength++;
            if (_currentWayLength > _minWayLength && SelectRandom(_changeWayFrequency))
            {
                ChangeWay();
            }
            base.PlaceProps();
        }

        protected override void Start()
        {
            _platformInstances = InstantiatePrefabs<Platform>(_platformPrefabs, _maxCountPlatform);
            _obstacleInstances = InstantiatePrefabs<Obstacle>(_obstaclePrefabs, _maxCountObstacle);
            _rampsInstances = InstantiatePrefabs<Ramp>(_rampPrefabs, _maxCountRamp);
            for (var road = 0; road < _countRoad; road++)
                _lastObstaclesPosition.Add(0);
            base.Start();
        }

        private void RebuildRow(Row row)
        {
            ClearRow(row);
            for (var road = 0; road < _countRoad; road++)
            {
                float zPosition = _instances[_fierstSegmentIndex].position.z;
                if (road != _currentWay && road != _currentWaySecond)
                {
                    if (_turn && SelectRandom(_rampFrequency))
                        PlaceObject(row, _rampsInstances, road, 0, 0, zPosition);
                    else
                        PlaceObject(row, _platformInstances, road, 0, 0, zPosition);
                }
                else if (zPosition >= _lastObstaclesPosition[road] + _obstacleMinDistance && SelectRandom(_obstacleFrequency))
                {
                    PlaceObject(row, _obstacleInstances, road, 0, 0, zPosition);
                }
            }
            _turn = false;
        }

        private void ClearRow(Row row)
        {
            ReturnInPool(row.Platforms, _platformInstances);
            ReturnInPool(row.Obstacles, _obstacleInstances);
            ReturnInPool(row.Ramps, _rampsInstances);
            //ReturnInPool(row.Coins, );
            row.Platforms.Clear();
            row.Obstacles.Clear();
            row.Coins.Clear();
        }

        private void ReturnInPool<T>(List<T> source, List<T> target) where T: InstantiatedObstacle
        {
            foreach (T platform in source)
            {
                target.Add(platform);
                platform.gameObject.SetActive(false);
                platform.transform.parent = null;
            }
        }

        private void ChangeWay()
        {
            int lastWay = _currentWay;
            int lastWaySecond = _currentWaySecond;
            _currentWay = Random.Range(0, _countRoad);
            if (_currentWay == _player.CurrentRoad)
                _currentWay = MoveWay(_currentWay);
            _currentWaySecond = Random.Range(0, _countRoad);
            if (_currentWaySecond == _player.CurrentRoad)
                _currentWaySecond = MoveWay(_currentWaySecond);
            if (!(lastWay == _currentWay && lastWaySecond == _currentWaySecond))
            {
                _lastSegmentPosition += _minTurnLength;
                _turn = true;
            }        
        }

        private int MoveWay(int way)
        {
            if (SelectRandom(50))
            {
                if (way + 1 <= _countRoad)
                    way++;
                else
                    way--;
            }
            else if (way - 1 >= 0)
                way--;
            else
                way++;
            return way;
        }

        private void PlaceObject<T>(Row row, List<T> instancers, int roadIndex, float indent, float height, float zPosition) where T : InstantiatedObstacle
        {
            if (zPosition >= _lastObstaclesPosition[roadIndex] && instancers.Count > 0)
            {
                T selected = instancers[Random.Range(0, instancers.Count)];
                instancers.Remove(selected);
                row.AddProp(selected);
                selected.transform.SetParent(row.transform);
                selected.gameObject.SetActive(true);
                selected.transform.localPosition = new Vector3((roadIndex - (_countRoad - 1) / 2) * _roadWidth, height, indent);
                _lastObstaclesPosition[roadIndex] = zPosition + selected.Properties.Length;
            }
        }

        //private void PlaceObject(Transform instance, int roadIndex, float indent, float height)
        //{
        //    instance.gameObject.SetActive(true);
        //    instance.localPosition = new Vector3((roadIndex - (_countRoad - 1) / 2) * _roadWidth, height, indent);
        //}

        private List<T> InstantiatePrefabs<T>(List<ObstacleProperties> prefabs, int maxCount) where T : InstantiatedObstacle
        {
            List<T> instances = new List<T>();
            foreach (ObstacleProperties obstacle in prefabs)
            {
                for (var count = 0; count < maxCount * (_countRoad - 1); count++)
                {
                    Transform instance = Instantiate(obstacle.Prefab, transform.position, Quaternion.identity);
                    instance.SetParent(transform);
                    instance.gameObject.SetActive(false);
                    if (instance.TryGetComponent(out T properties))
                        instances.Add(properties);
                    else
                        Debug.LogError($"{obstacle} Missing InstantiatedObstacle");
                }
            }
            return instances;
        }


        private bool SelectRandom(float chance) => Random.Range(1, 101) <= chance;


        //[SerializeField] private int _fierstIndent;

        //protected override void Start()
        //{
        //    _lastSegmentPosition = _fierstIndent;
        //    base.Start();
        //}

        //protected override void MoveLastSegment()
        //{
        //    if (_instances[_fierstSegmentIndex].TryGetComponent(out Wall wall))
        //        wall.RandomizeWall();
        //    base.MoveLastSegment();
        //}
    }
}
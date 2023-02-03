using Prop;
using System.Collections.Generic;
using UnityEngine;

namespace ObstacleGenerator
{
    public class ObstacleAppointer : PropReplacer
    {
        [SerializeField] private List<ObstacleProperties> _platformPrefabs = new List<ObstacleProperties>();
        [SerializeField, Min(1)] private int _maxCountPlatform = 40;
        [SerializeField] private List<ObstacleProperties> _obstaclePrefabs = new List<ObstacleProperties>();
        [SerializeField, Min(1)] private int _maxCountObstacle = 40;
        [SerializeField] private List<ObstacleProperties> _rampPrefabs = new List<ObstacleProperties>();
        [SerializeField, Min(1)] private int _maxCountRamp = 3;
        [SerializeField] private PropProperties _coinPrefab;
        [SerializeField, Min(1)] private int _maxCountCoin = 20;
        [SerializeField, Range(0, 100)] private float _changeWayFrequency;
        [SerializeField, Range(0, 100)] private float _rampFrequency;
        [SerializeField, Range(0, 100)] private float _obstacleFrequency;
        [SerializeField, Range(0, 100)] private float _coinFrequency;
        [SerializeField, Min(0)] private float _obstacleMinDistance;
        [SerializeField, Min(1)] private int _minWayLength;
        [SerializeField, Min(1)] private float _minTurnLength;
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

        protected override void ShowProps()
        {
            if (_instances[_rowIndex].TryGetComponent(out Row row))
                RebuildRow(row);
            base.ShowProps();
        }

        protected override void PlaceProps()
        {
            _currentWayLength++;
            if (_currentWayLength > _minWayLength && SelectRandom(_changeWayFrequency))
                ChangeWay();
            base.PlaceProps();
        }

        protected override void Start()
        {
            _platformInstances = InstantiatePrefabs<Platform, ObstacleProperties>(_maxCountPlatform, _platformPrefabs.ToArray());
            _obstacleInstances = InstantiatePrefabs<Obstacle, ObstacleProperties>(_maxCountObstacle, _obstaclePrefabs.ToArray());
            _rampsInstances = InstantiatePrefabs<Ramp, ObstacleProperties>(_maxCountRamp, _rampPrefabs.ToArray());
            _coinsInstances = InstantiatePrefabs<Coin, PropProperties>(_maxCountCoin, _coinPrefab);
            for (var road = 0; road < _countRoad; road++)
                _lastObstaclesPosition.Add(0);
            base.Start();
        }

        private void RebuildRow(Row row)
        {
            ClearRow(row);
            for (var road = 0; road < _countRoad; road++)
            {
                float zPosition = _instances[_rowIndex].position.z;
                InstantiatedObstacle obstacle = null;
                if (road != _currentWay && road != _currentWaySecond)
                {
                    if (_turn && SelectRandom(_rampFrequency))
                        obstacle = PlaceObstacle(row, _rampsInstances, road, 0, 0, zPosition);
                    else
                        obstacle = PlaceObstacle(row, _platformInstances, road, 0, 0, zPosition);
                }
                else if (zPosition >= _lastObstaclesPosition[road] + _obstacleMinDistance && SelectRandom(_obstacleFrequency))
                    obstacle = PlaceObstacle(row, _obstacleInstances, road, 0, 0, zPosition);
                else if (zPosition >= _lastObstaclesPosition[road] + _obstacleMinDistance && SelectRandom(_coinFrequency))
                    PlaceObject(row, _coinsInstances, road, 0, 0);
                if (obstacle != null && SelectRandom(_coinFrequency))
                    SetCoinsOnObstacle(row, road, obstacle);
            }
            _turn = false;
        }

        private void ClearRow(Row row)
        {
            ReturnInPool(row.Platforms, _platformInstances);
            ReturnInPool(row.Obstacles, _obstacleInstances);
            ReturnInPool(row.Ramps, _rampsInstances);
            ReturnInPool(row.Coins, _coinsInstances);
            row.ClearProps();
        }

        private void ReturnInPool<T>(List<T> source, List<T> target) where T: InstantiatedProp
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
                if (way + 1 < _countRoad)
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

        private T PlaceObject<T>(Row row, List<T> instancers, int roadIndex, float indent, float height) where T : InstantiatedProp
        {
            if (instancers.Count == 0)
                return null;
            T selected = instancers[Random.Range(0, instancers.Count)];
            instancers.Remove(selected);
            row.AddProp(selected);
            selected.transform.SetParent(row.transform);
            selected.gameObject.SetActive(true);
            selected.transform.localPosition = new Vector3((roadIndex - (_countRoad - 1) / 2) * _roadWidth, height, indent);
            return selected;
        }

        private T PlaceObstacle<T>(Row row, List<T> instancers, int roadIndex, float indent, float height, float zPosition) where T : InstantiatedObstacle
        {
            if (zPosition < _lastObstaclesPosition[roadIndex])
                return null;
            T selected = PlaceObject(row, instancers, roadIndex, indent, height);
            if (selected != null)
                _lastObstaclesPosition[roadIndex] = zPosition + selected.Properties.Length;
            return selected;
        }

        private void SetCoinsOnObstacle(Row row, int roadIndex, InstantiatedObstacle obstacle)
        {
            if (obstacle.Properties is ObstaclePropertiesCoins)
            {
                ObstaclePropertiesCoins properties = obstacle.Properties as ObstaclePropertiesCoins;
                for (var indent = 0; indent < properties.Length; indent++) 
                    PlaceObject(row, _coinsInstances, roadIndex, indent, properties.CoinSpawnHeight);
            }
        }

        private List<T> InstantiatePrefabs<T, U>(int maxCount, params U[] prefabs) where T : InstantiatedProp where U : PropProperties
        {
            List<T> instances = new List<T>();
            foreach (U obstacle in prefabs)
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
    }
}
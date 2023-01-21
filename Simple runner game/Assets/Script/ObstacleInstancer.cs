using Obstacles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class ObstacleInstancer : PropInstancer
    {
        [SerializeField] private List<Obstacle> _platformPrefabs = new List<Obstacle>();
        [SerializeField, Min(1)] private int _maxCountPlatform = 40;
        [SerializeField] private List<Obstacle> _obstaclePrefabs = new List<Obstacle>();
        [SerializeField, Min(1)] private int _maxCountObstacle = 40;
        [SerializeField, Range(0, 100)] private float _changeWayFrequency;
        [Space]
        [SerializeField, Min(0)] private float _roadWidth = 4;
        [SerializeField, Min(1)] private int _countRoad = 3;

        private List<InstantiatedObstacle> _platformInstances = new List<InstantiatedObstacle>();
        private List<InstantiatedObstacle> _obstacleInstances = new List<InstantiatedObstacle>();
        private List<float> _lastObstaclesPosition = new List<float>();
        private int _currentWay = 1;
        private int _currentWaySecond = 1;

        protected override void Show()
        {
            if (_instances[_fierstSegmentIndex].TryGetComponent(out Row row))
            {
                ClearRow(row);
                for (var road = 0; road < _countRoad; road++)
                {
                    float zPosition = _instances[_fierstSegmentIndex].position.z;
                   // Debug.Log($"{zPosition}|||{_lastObstaclesPosition[road] }");
                   //if (zPosition < _lastObstaclesPosition[road] && road != 1)
                   //     Debug.Log("Àó");
                    if (road != _currentWay && road != _currentWaySecond && zPosition >= _lastObstaclesPosition[road] && _platformInstances.Count > 0)
                    {
                        
                        InstantiatedObstacle selected = _platformInstances[Random.Range(0, _platformInstances.Count)];
                        _platformInstances.Remove(selected);
                        row.Platforms.Add(selected);
                        selected.transform.SetParent(row.transform);
                        PlaceObject(selected.transform, road, 0/*_instances[_fierstSegmentIndex].position.z*/, 0);
                        _lastObstaclesPosition[road] = zPosition + selected.Properties.Length;
                    }
                }
            }
            //if (Allowed(_changeWayFrequency))
            //    ChangeWay();
            //else
            //{ 
            //}
            base.Show();
        }

        protected override void Start()
        {
            _platformInstances = InstantiatePrefabs(_platformPrefabs, _maxCountPlatform);
            _obstacleInstances = InstantiatePrefabs(_obstaclePrefabs, _maxCountObstacle);
            for (var road = 0; road < _countRoad; road++)
                _lastObstaclesPosition.Add(0);
            //    _freePlatforms = _platformInstances;
            //   _freeObstacles = _obstacleInstances;
            base.Start();
        }

        private void ClearRow(Row row)
        {
            foreach (InstantiatedObstacle platform in row.Platforms)
            {
                _platformInstances.Add(platform);
                platform.gameObject.SetActive(false);
                platform.transform.parent = null;
            }
            foreach (InstantiatedObstacle obstacle in row.Obstacles)
            { 
                _obstacleInstances.Add(obstacle);
                obstacle.gameObject.SetActive(false);
                obstacle.transform.parent = null;
            }
         //   foreach (InstantiatedObstacle coin in row.Coins)
         //       _freePlatforms.Add(coin);
            row.Platforms.Clear();
            row.Obstacles.Clear();
            row.Coins.Clear();
        }

        private void ChangeWay()
        {
            
        }

        private void PlaceObject(Transform instance, int roadIndex, float indent, float height)
        {
            instance.gameObject.SetActive(true);
            instance.localPosition = new Vector3((roadIndex - (_countRoad - 1) / 2) * _roadWidth, height, indent);
        }

        private List<InstantiatedObstacle> InstantiatePrefabs(List<Obstacle> prefabs, int maxCount)
        {
            List<InstantiatedObstacle> instances = new List<InstantiatedObstacle>();
            foreach (Obstacle obstacle in prefabs)
            {
                for (var count = 0; count < maxCount * (_countRoad - 1); count++)
                {
                    Transform instance = Instantiate(obstacle.Prefab, transform.position, Quaternion.identity);
                    instance.SetParent(transform);
                    instance.gameObject.SetActive(false);
                    if (instance.TryGetComponent(out InstantiatedObstacle properties))
                        instances.Add(properties);
                    else
                        Debug.LogError($"{obstacle} Missing InstantiatedObstacle");
                }
            }
            return instances;
        }


        private bool Allowed(float chance) => Random.Range(1, 101) <= chance;


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
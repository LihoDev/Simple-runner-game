using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    public class Row : MonoBehaviour
    {
        public List<Platform> Platforms { get; private set; } = new List<Platform>();
        public List<Ramp> Ramps { get; private set; } = new List<Ramp>();
        public List<Obstacle> Obstacles { get; private set; } = new List<Obstacle>();
        public List<Coin> Coins { get; private set; } = new List<Coin>();
        
        public void AddProp<T>(T prop) where T: InstantiatedObstacle
        {
            if (prop is Platform)
                Platforms.Add(prop as Platform);
            else if (prop is Ramp)
                Ramps.Add(prop as Ramp);
            else if (prop is Obstacle)
                Obstacles.Add(prop as Obstacle);
            else if (prop is Coin)
                Coins.Add(prop as Coin);
        }
        //[SerializeField] private Transform _coinPrefab;
        //[SerializeField, Min(0)] private int _maxCoinOnScene;
        //[SerializeField, Min(0)] private float _coinFrequency;
        //[SerializeField, Min(0)] private float _coinHieghtOnCar;
        //[SerializeField, Min(0)] private int _maxCoinLineLength;
        //[Space]
        //[SerializeField] private List<Transform> _aislesPrefabs;
        //[SerializeField] private List<Transform> _deadEndsPrefabs;
        //[SerializeField] private List<Transform> _carsPrefabs;
        //[SerializeField] private Transform _rampPrefab;
        //[SerializeField, Min(0)] private float _roadWidth = 4;
        //[SerializeField, Min(1)] private int _countRoad = 3;
        //[SerializeField, Min(0)] private int _maxTrainLength = 1;
        //[SerializeField, Min(1)] private int _carLength = 1;

        //private List<Transform> _hiddenRamps = new List<Transform>();
        //private List<Transform> _coins = new List<Transform>();
        //private List<List<Transform>> _hiddenCars = new List<List<Transform>>();
        //private List<List<Transform>> _hiddenAisles = new List<List<Transform>>();
        //private List<List<Transform>> _hiddenDeadEnds = new List<List<Transform>>();
        //private List<List<Transform>> _active = new List<List<Transform>>();

        //private int _currentCoinIndex = 0;

        //[ContextMenu("Randomize")]
        //public void RandomizeWall()
        //{
        //    HideWall();
        //    for (var road = 0; road < _countRoad; road++)
        //    {
        //        switch (Random.Range(0, 4))
        //        {
        //            case 0:
        //                if (Random.Range(0, 2) < 1)
        //                {
        //                    PlaceObject(_hiddenDeadEnds[road][Random.Range(0, _deadEndsPrefabs.Count)], road, 0, 0);
        //                }
        //                else
        //                {
        //                    PlaceObject(_hiddenAisles[road][Random.Range(0, _aislesPrefabs.Count)], road, 0, 0);
        //                    PlaceCoinLine(road, _maxCoinLineLength, 0, _coinFrequency);
        //                }
        //                break;
        //            case 1:
        //                PlaceTrain(road);
        //                break;
        //            case 2:
        //                PlaceCoinLine(road, _maxCoinLineLength, 0, _coinFrequency);
        //                break;
        //        }
        //    }
        //    int guaranteedPassage = Random.Range(0, _countRoad);
        //    HideOnRoad(guaranteedPassage);
        //    PlaceObject(_hiddenAisles[guaranteedPassage][Random.Range(0, _aislesPrefabs.Count)], guaranteedPassage, 0, 0);
        //}

        //public void HideWall()
        //{
        //    for (var road = 0; road < _countRoad; road++)
        //    {
        //        for (var i = 0; i < _active[road].Count; i++)
        //        {
        //            _active[road][i].gameObject.SetActive(false);
        //        }
        //        _active[road].Clear();
        //    }
        //    _currentCoinIndex = 0;
        //}

        //private void PlaceCoinLine(int roadIndex, int length, float height, float startIndent)
        //{
        //    float indent = startIndent;
        //    for (var i = 0; i < Random.Range(0, length); i++)
        //    {
        //        PlaceCoin(roadIndex, indent, height);
        //        indent += _coinFrequency;
        //    }
        //}

        //private void PlaceCoin(int roadIndex, float indent, float height)
        //{
        //    if (_currentCoinIndex >= _maxCoinOnScene)
        //        return;
        //    _coins[_currentCoinIndex].parent = transform;
        //    PlaceObject(_coins[_currentCoinIndex], roadIndex, indent, height);
        //    _currentCoinIndex++;
        //}

        //private void PlaceTrain(int roadIndex)
        //{
        //    int indent = 0;
        //    bool ramp = false;
        //    bool withCoins = Random.Range(0, 2) > 0;
        //    if (Random.Range(0, 2) > 0)
        //    {
        //        PlaceObject(_hiddenRamps[roadIndex], roadIndex, 0, 0);
        //        indent += _carLength;
        //        ramp = true;
        //    }
        //    for (var i=0; i < Random.Range(0, _maxTrainLength); i++)
        //    {
        //        if (ramp && withCoins)
        //            PlaceCoinLine(roadIndex, _carLength, _coinHieghtOnCar, _carLength);
        //        PlaceObject(_hiddenCars[roadIndex][i], roadIndex, indent, 0);
        //        indent += _carLength;
        //    }
        //}

        //private void HideOnRoad(int roadIndex)
        //{
        //    for (var i = 0; i < _active[roadIndex].Count; i++)
        //    {
        //        _active[roadIndex][i].gameObject.SetActive(false);

        //    }
        //    _active[roadIndex].Clear();
        //}

        //private void PlaceObject(Transform instance, int roadIndex, float indent, float height)
        //{
        //    _active[roadIndex].Add(instance);
        //    instance.gameObject.SetActive(true);
        //    instance.localPosition = new Vector3((roadIndex - (_countRoad - 1) / 2) * _roadWidth, height, indent);
        //}

        //[ContextMenu("Instantiate Prefabs")]
        //private void InstantiatePrefabs()
        //{
        //    if (_aislesPrefabs.Count == 0)
        //    {
        //        Debug.LogError("Aisles prefabs missing");
        //        return;
        //    }
        //    if (_deadEndsPrefabs.Count == 0)
        //    {
        //        Debug.LogError("DeadEnds prefabs missing");
        //        return;
        //    }
        //    if (_rampPrefab == null && _maxTrainLength > 0)
        //    {
        //        Debug.LogError("Ramp prefab missing");
        //        return;
        //    }
        //    if (_carsPrefabs.Count == 0 && _maxTrainLength > 0)
        //    {
        //        Debug.LogError("Cats prefabs missing");
        //        return;
        //    }
        //    for (var road = 0; road < _countRoad; road++)
        //    {
        //        _active.Add(new List<Transform>());
        //        List<Transform> aisles = new List<Transform>();
        //        List<Transform> deadEnds = new List<Transform>();
        //        List<Transform> cars = new List<Transform>();
        //        _hiddenAisles.Add(aisles);
        //        _hiddenDeadEnds.Add(deadEnds);
        //        _hiddenCars.Add(cars);
        //        foreach (Transform prefab in _aislesPrefabs)
        //            aisles.Add(InstantiatePrefab(prefab));
        //        foreach (Transform prefab in _deadEndsPrefabs)
        //            deadEnds.Add(InstantiatePrefab(prefab));
        //        for (var i = 0; i < _maxTrainLength; i++)
        //            cars.Add(InstantiatePrefab(_carsPrefabs[Random.Range(0, _carsPrefabs.Count)]));
        //        _hiddenRamps.Add(InstantiatePrefab(_rampPrefab));
        //    }
        //    for (var i = 0; i < _maxCoinOnScene; i++)
        //        _coins.Add(InstantiatePrefab(_coinPrefab));
        //}

        //private Transform InstantiatePrefab(Transform prefab)
        //{
        //    Transform instance = Instantiate(prefab, transform.position, Quaternion.identity);
        //    instance.parent = transform;
        //    instance.gameObject.SetActive(false);
        //    return instance;
        //}

        //private void Start()
        //{
        //    InstantiatePrefabs();
        //    RandomizeWall();
        //}
    }
}
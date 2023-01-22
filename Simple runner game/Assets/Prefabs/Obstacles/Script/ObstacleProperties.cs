using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacle", menuName = "Obstacles/NewObstacle", order = 1)]
public class ObstacleProperties : ScriptableObject
{
    public int Length { get => _length; private set { _length = value; } }
    public Transform Prefab { get => _prefab; private set { _prefab = value; } }
    public float CoinSpawnHeight { get => _coinSpawnHeight; private set { _coinSpawnHeight = value; } }
    [SerializeField] private Transform _prefab;
    [SerializeField, Min(1)] private int _length = 1;
    [SerializeField, Min(0)] private float _coinSpawnHeight = 1;
}

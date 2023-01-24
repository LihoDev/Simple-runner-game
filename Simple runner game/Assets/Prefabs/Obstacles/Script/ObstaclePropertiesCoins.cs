using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacle with coins", menuName = "Obstacles/NewObstacleCoins", order = 3)]
public class ObstaclePropertiesCoins : ObstacleProperties
{
    public float CoinSpawnHeight { get => _coinSpawnHeight; private set { _coinSpawnHeight = value; } }
    [SerializeField, Min(0)] private float _coinSpawnHeight = 1;
}

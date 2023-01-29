using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacle", menuName = "Obstacles/NewObstacle", order = 2)]
public class ObstacleProperties : PropProperties
{
    public int Length { get => _length; private set { _length = value; } }
    [SerializeField, Min(1)] private int _length = 1;
}

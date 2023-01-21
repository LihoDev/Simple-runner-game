using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedObstacle : MonoBehaviour
{
    public Obstacle Properties { get => _properties; private set { _properties = value; } }
    [SerializeField] private Obstacle _properties;
}

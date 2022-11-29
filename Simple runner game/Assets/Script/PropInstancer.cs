using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class PropInstancer : MonoBehaviour
    {
        [SerializeField] private Transform _prefab;
        [SerializeField, Min(1)] private int _maxCountOnScene;
    }
}
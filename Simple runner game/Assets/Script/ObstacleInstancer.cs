using Obstacles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class ObstacleInstancer : PropInstancer
    {
        [SerializeField] private int _fierstIndent;

        protected override void Start()
        {
            _lastSegmentPosition = _fierstIndent;
            base.Start();
        }

        protected override void MoveLastSegment()
        {
            if (_instances[_fierstSegmentIndex].TryGetComponent(out Wall wall))
                wall.RandomizeWall();
            base.MoveLastSegment();
        }
    }
}
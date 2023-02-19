using System.Collections.Generic;
using UnityEngine;

namespace ObstacleGenerator
{
    public class RoadReplacer : PropReplacer
    {
        private List<Transform> _useRoad = new List<Transform>();

        public override void ResetDistance(float distance)
        {
            base.ResetDistance(distance);
            foreach (Transform road in _useRoad)
                road.position -= new Vector3(0, 0, distance);
        }

        protected override void PlaceProps()
        {
            if (_rowIndex != -1)
            {
                if (_useRoad.Count >= MaxCountActive)
                {
                    _instances.Add(_useRoad[0]);
                    _useRoad.Remove(_useRoad[0]);
                }
                _useRoad.Add(_instances[_rowIndex]);
                base.PlaceProps();
                _instances.Remove(_instances[_rowIndex]);
            }
        }

        protected override void MoveRowIndex()
        {
            if (_instances.Count == 0)
                _rowIndex = -1;
            else
                _rowIndex = Random.Range(0, _instances.Count);
        }
    }
}

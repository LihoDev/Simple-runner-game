using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class RoadReplacer : ObjectReplacer
    {
        private List<Transform> _useRoad = new List<Transform>();

        protected override void Start()
        {
            base.Start();
            //_useRoad = _rows.GetRange(0, _rows.Count);
        }

        protected override void PlaceProps()
        {
            if (_rowIndex != -1)
            {
                if (_useRoad.Count >= MaxCountActive)
                {
                    _rows.Add(_useRoad[0]);
                    _useRoad.Remove(_useRoad[0]);
                }
                _useRoad.Add(_rows[_rowIndex]);
                base.PlaceProps();
                _rows.Remove(_rows[_rowIndex]);
            }
        }

        protected override void MoveRowIndex()
        {
            if (_rows.Count == 0)
                _rowIndex = -1;
            else
                _rowIndex = Random.Range(0, _rows.Count);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class BendOver : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _bendOverTime;
        [SerializeField] private Vector3 _bendOverCollisionSize;

        //private IEnumerator BendOver()
        //{
        //    if (_mesh != null)
        //    {
        //        _currentState = States.bendOver;
        //        float time = 0;
        //        _mesh.localScale = _bendOverCollisionSize;
        //        while (time < _bendOverTime)
        //        {
        //            time += Time.deltaTime;
        //            yield return null;
        //        }
        //        _mesh.localScale = Vector3.one;
        //        _moving = null;
        //        _currentState = States.idle;
        //    }
        //    else
        //        Debug.LogError("Mesh missing");
        //}
    }
}
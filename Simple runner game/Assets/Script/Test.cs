using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Vector3 _previousStep = Vector3.zero;

    private bool TouchGround()
    {
        return Physics.Raycast(transform.localPosition, -transform.up, out RaycastHit hit, Vector3.Distance(transform.localPosition, _previousStep) * 1.02f) && hit.collider != null;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(TouchGround());
        _previousStep = transform.localPosition;    
    }
}

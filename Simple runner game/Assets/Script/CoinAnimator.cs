using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimator : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;

    private void FixedUpdate()
    {
        transform.Rotate(0, _speed * Time.fixedDeltaTime, 0, Space.Self);
    }
}

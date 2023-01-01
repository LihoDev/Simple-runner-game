using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _amount;

    private Vector3 _originalPos;

    public void StartShake()
    {
        _originalPos = gameObject.transform.localPosition;
        StopAllCoroutines();
        StartCoroutine(Shaking());
    }

    private IEnumerator Shaking()
    {
        while (_duration > 0)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * _amount;
            _duration -= Time.deltaTime;
            yield return null;
        }
        transform.localPosition = _originalPos;
    }
}

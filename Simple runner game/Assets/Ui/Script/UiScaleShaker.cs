using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UiScaleShaker : MonoBehaviour
{
    [SerializeField] private bool _axisX;
    [SerializeField] private bool _axisY;
    [SerializeField] private bool _axisZ;
    [SerializeField] private AnimationCurve _sizeCurve;
    [SerializeField, Min(0)] private float _speed;

    private RectTransform rectTransform;

    public void ShakeScale()
    {
        StartCoroutine(AnimateSize());
    }

    protected virtual IEnumerator AnimateSize()
    {
        float time = 0;
        while (time < 1)
        {
            time += _speed * Time.deltaTime;
            float value = _sizeCurve.Evaluate(time);
            rectTransform.localScale = new Vector3(_axisX? value: 1, _axisY ? value : 1, _axisZ ? value : 1);
            yield return null;
        }
        rectTransform.localScale = Vector2.one;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}

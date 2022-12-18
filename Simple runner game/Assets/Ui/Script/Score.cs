using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Score : MonoBehaviour
{
    public int Value { get => (int)_value;}

    [SerializeField] private TMP_Text _text;

    private float _value;

    private void Update()
    {
        _value += Time.deltaTime;
        _text.text = Value.ToString();
    }
}

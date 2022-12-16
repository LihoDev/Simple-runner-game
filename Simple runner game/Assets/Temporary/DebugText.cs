using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private static DebugText _instance;

    public static void Show(string text)
    {
        _instance._text.text = text;
    }

    private void Awake()
    {
        _instance = this;
    }
}

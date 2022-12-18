using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalStats : MonoBehaviour
{
    public int Coins { get; private set; }
    public int Score { get; private set; }

    [SerializeField] private TMP_Text _coinsOutput;
    [SerializeField] private TMP_Text _scoreOutput;
    [SerializeField] private SaveData _saveData;

    public void RefreshValues()
    {
        Coins = _saveData.GetCoins();
        _coinsOutput.text = Coins.ToString();
        Score = _saveData.GetScore();
        _scoreOutput.text = Score.ToString();
    }

    private void Start()
    {
        RefreshValues();
    }

    
}

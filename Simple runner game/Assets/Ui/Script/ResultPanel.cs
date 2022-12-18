using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private CollectCoins _collectCoins;
    [SerializeField] private Score _score;
    [SerializeField] private TotalStats _totalStats;
    [SerializeField] private SaveData _saveData;
    [SerializeField, Min(0)] private float _animateSpeed = 0;
    [SerializeField] private GameObject _recordText;

    public void ShowResults()
    {
        gameObject.SetActive(true);
        if (_score.Value > _totalStats.Score)
        {
            ShowRecord();
            _saveData.SaveScore(_score.Value);
        }
        _saveData.SaveCoins(_totalStats.Coins + _collectCoins.Count);
        _totalStats.RefreshValues();
        StartCoroutine(ShowValues());
    }

    private IEnumerator AnimateOutput(TMP_Text output, int count)
    {
        float currentSecond = 0;
        while (currentSecond < _animateSpeed)
        {
            output.text = ((int)(currentSecond / _animateSpeed * count)).ToString();
            currentSecond += Time.deltaTime;
            yield return null;
        }
        output.text = count.ToString();
    } 

    private IEnumerator ShowValues()
    {
        yield return AnimateOutput(_scoreText, _score.Value);
        if (_scoreText.gameObject.TryGetComponent(out UiScaleShaker scoreShaker))
            scoreShaker.ShakeScale();
        yield return AnimateOutput(_coinsText, _collectCoins.Count);
        if (_coinsText.gameObject.TryGetComponent(out UiScaleShaker coinShaker))
            coinShaker.ShakeScale();
    }

    private void ShowRecord()
    {
        _recordText.SetActive(true);
    }
}

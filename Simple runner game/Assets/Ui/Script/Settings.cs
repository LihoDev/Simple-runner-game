using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _soundEffects;
    [SerializeField] private SaveData _saveData;

    public void ShowWindow()
    {
        gameObject.SetActive(true);
    }

    public void HideWindow()
    {
        _saveData.SaveMusicValume(_music.value);
        _saveData.SaveSoundEffectsValume(_soundEffects.value);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        _music.value = _saveData.GetMusicValume();
        _soundEffects.value = _saveData.GetSoundEffectsValume();
    }
}

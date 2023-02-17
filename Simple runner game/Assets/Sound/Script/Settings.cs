using Data;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private string _musicAudioMixerParameter = "Music";
    [SerializeField] private string _soundEffectsAudioMixerParameter = "Sound Effects";

    [SerializeField] private SaveData _saveData;
    [SerializeField] private AudioMixer _audioMixer;

    public void SaveAudioSettings(float music, float soundEffects)
    {
        _saveData.SaveMusicValume(music);
        _saveData.SaveSoundEffectsValume(soundEffects);
    }

    public float GetMusicVolume()
    {
        return _saveData.GetMusicVolume();
    }

    public void SetMusicVolume(float value)
    {
        _audioMixer.SetFloat(_musicAudioMixerParameter, value);
    }

    public float GetSoundEffectsVolume()
    {
        return _saveData.GetSoundEffectsVolume();
    }

    public void SetSoundEffectsVolume(float value)
    {
        _audioMixer.SetFloat(_soundEffectsAudioMixerParameter, value);
    }

    private void Start()
    {
        LoadAudioMixerValues();
    }

    private void LoadAudioMixerValues()
    {
        _audioMixer.SetFloat(_musicAudioMixerParameter, _saveData.GetMusicVolume());
        _audioMixer.SetFloat(_soundEffectsAudioMixerParameter, _saveData.GetSoundEffectsVolume());
    }
}

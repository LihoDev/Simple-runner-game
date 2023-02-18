using UnityEngine;

namespace Data
{
    public class SaveData : MonoBehaviour
    {
        [SerializeField] private string _coinsKey = "Coins";
        [SerializeField] private string _scoreKey = "Score";
        [SerializeField] private string _musicVolumeKey = "Music";
        [SerializeField] private string _soundEffectsVolumeKey = "SoundEffects";
        [SerializeField] private int _defaultVolumeValue = 0;

        public int GetCoins()
        {
            return PlayerPrefs.GetInt(_coinsKey, 0);
        }

        public void SaveCoins(int count)
        {
            PlayerPrefs.SetInt(_coinsKey, count);
        }

        public int GetScore()
        {
            return PlayerPrefs.GetInt(_scoreKey, 0);
        }

        public void SaveScore(int count)
        {
            PlayerPrefs.SetInt(_scoreKey, count);
        }

        public float GetMusicVolume()
        {
            return PlayerPrefs.GetFloat(_musicVolumeKey, _defaultVolumeValue);
        }

        public void SaveMusicValume(float value)
        {
            PlayerPrefs.SetFloat(_musicVolumeKey, value);
        }

        public float GetSoundEffectsVolume()
        {
            return PlayerPrefs.GetFloat(_soundEffectsVolumeKey, _defaultVolumeValue);
        }

        public void SaveSoundEffectsValume(float value)
        {
            PlayerPrefs.SetFloat(_soundEffectsVolumeKey, value);
        }

        [ContextMenu("Clear data")]
        private void ClearData()
        {
            PlayerPrefs.DeleteAll();
            //SaveScore(0);
            //SaveCoins(0);
            //SaveMusicValume(0);
            //SaveSoundEffectsValume(0);
        }
    }
}
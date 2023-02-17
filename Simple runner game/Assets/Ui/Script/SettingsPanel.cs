using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Slider _music;
        [SerializeField] private Slider _soundEffects;
        [SerializeField] private Settings _settings;

        public void ShowWindow()
        {
            gameObject.SetActive(true);
        }

        public void HideWindow()
        {
            _settings.SaveAudioSettings(_music.value, _soundEffects.value);
            gameObject.SetActive(false);
        }

        private void Start()
        {
            _music.value = _settings.GetMusicVolume();
            _soundEffects.value = _settings.GetSoundEffectsVolume();
        }
    }
}
using UnityEngine;

namespace Ui
{
    public class Pause : MonoBehaviour
    {
        public void ShowWindow()
        {
            gameObject.SetActive(true);
            PauseGame();
        }

        public void HideWindow()
        {
            gameObject.SetActive(false);
            ResumeGame();
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}
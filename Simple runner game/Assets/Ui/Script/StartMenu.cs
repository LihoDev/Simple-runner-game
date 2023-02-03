using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class StartMenu : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RunnerLauncher _runnerLauncher;
        [SerializeField] private Animation _animation;

        public void OnPointerClick(PointerEventData eventData)
        {
            StartGame();
        }

        private void StartGame()
        {
            _runnerLauncher.StartRun();
            _animation.Play();
            gameObject.SetActive(false);
        }
    }
}
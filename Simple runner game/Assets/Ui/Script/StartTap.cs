using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class StartTap : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RunnerLauncher _runnerLauncher;
        [SerializeField] private Animation _animation;


        public void OnPointerDown(PointerEventData eventData)
        {
            StartGame();
            Destroy(gameObject);
        }

        private void StartGame()
        {
            _runnerLauncher.StartRun();
            _animation.Play();
            gameObject.SetActive(false);
        }
    }
}
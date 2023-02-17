using Player;
using ObstacleGenerator;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Ui;

public class RunnerLauncher : MonoBehaviour
{
    [SerializeField] private JumpMovement _jumpMovement;
    [SerializeField] private SideMovement _sideMovement;
    [SerializeField] private ForwardMovement _forwardMovement;
    [SerializeField] private SwipeDetector _swipeDetector;
    [SerializeField] private MaterialCurveMover _curvedWorld;
    [SerializeField] private ResultPanel _resultPanel;
    [SerializeField] private GameObject _statisticsPanel;
    [SerializeField] private UnityEvent OnStartRun;

    public void StopRun()
    {
        _sideMovement.StopMoving();
        _forwardMovement.enabled = false;
        _swipeDetector.gameObject.SetActive(false);
        _curvedWorld.enabled = false;
        _statisticsPanel.SetActive(false);
        _resultPanel.ShowResults();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartRun()
    {
        _forwardMovement.enabled = true;
        _swipeDetector.gameObject.SetActive(true);
        _curvedWorld.enabled = true;
        _statisticsPanel.SetActive(true);
        OnStartRun?.Invoke();
    }
}

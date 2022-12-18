using Player;
using Props;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RunnerLauncher : MonoBehaviour
{
    [SerializeField] private JumpMovement _jumpMovement;
    [SerializeField] private SideMovement _sideMovement;
    [SerializeField] private ForwardMovement _forwardMovement;
    [SerializeField] private SwipeDetector _swipeDetector;
    [SerializeField] private CurvedWorld _curvedWorld;
    [SerializeField] private ResultPanel _resultPanel;
    [SerializeField] private GameObject _statisticsPanel;
    [SerializeField] private UnityEvent OnStartRun;

    public void StopRun()
    {
        _jumpMovement.StopMoving();
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

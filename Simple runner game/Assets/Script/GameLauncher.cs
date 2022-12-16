using Player;
using Props;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour
{
    [SerializeField] private JumpMovement _jumpMovement;
    [SerializeField] private SideMovement _sideMovement;
    [SerializeField] private ForwardMovement _forwardMovement;
    [SerializeField] private SwipeDetector _swipeDetector;
    [SerializeField] private CurvedWorld _curvedWorld;
    [SerializeField] private GameObject _restartPanel;

    public void StopGame()
    {
        _jumpMovement.StopMoving();
        _sideMovement.StopMoving();
        _forwardMovement.enabled = false;
        _swipeDetector.enabled = false;
        _curvedWorld.enabled = false;
        _restartPanel.SetActive(true);
        Debug.Log("Game over");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

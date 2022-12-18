using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RunnerLauncher _runnerLauncher;
    [SerializeField] private Animation _animation;

    public void OnPointerClick(PointerEventData eventData)
    {
        _runnerLauncher.StartRun();
        _animation.Play();
        gameObject.SetActive(false);
    }
}

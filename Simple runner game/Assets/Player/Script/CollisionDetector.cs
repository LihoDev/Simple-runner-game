using Effects;
using Ui;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class CollisionDetector : MonoBehaviour
    {
        public int TouchCount { get; private set; }
        [SerializeField] private RunnerLauncher _runnerLauncher;
        [SerializeField] private SideMovement _sideMovement;
        [SerializeField] private Shaker _shaker;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _maxDistanceToGameOver = 1.2f;
        [SerializeField] private AnimationCaller _animationCaller;
        [SerializeField] private UnityEvent StopRun;
        [SerializeField] private UnityEvent Touch;
        [SerializeField] private int _maxCountTouch = 2;
        [SerializeField] private HpPanel _hpPanel;

        private void OnTriggerEnter(Collider collider)
        {
            DetermineTypeCollision(collider);
        }

        private void DetermineTypeCollision(Collider collider)
        {
            if ((_layer.value & (1 << collider.transform.gameObject.layer)) > 0)
            {
                float obstaclePosition = collider.transform.position.x;
                if (IsFullCollision(obstaclePosition) || TouchCount + 1 >= _maxCountTouch)
                {
                    _runnerLauncher.StopRun();
                    _animationCaller.CallColiision();
                    StopRun?.Invoke();
                }
                else
                {
                    if (transform.position.x < collider.transform.position.x)
                        _animationCaller.CallCollisionRight();
                    else
                        _animationCaller.CallCollisionLeft();
                    _sideMovement.AbortMoving();
                    TouchCount++;
                    _hpPanel.RemoveOne();
                    Touch?.Invoke();
                }
                _shaker.StartShake();
            }
        }

        public void RestoreHealth()
        {
            if (TouchCount > 0)
            {
                TouchCount--;
                _hpPanel.AddOne();
            }
        }

        private bool IsFullCollision(float obstaclePosition)
        {
            return Mathf.Abs(obstaclePosition - transform.position.x) < _maxDistanceToGameOver;
        }

        private void Start()
        {
            _hpPanel.InstantiateHpIcons(_maxCountTouch);
        }
    }
}

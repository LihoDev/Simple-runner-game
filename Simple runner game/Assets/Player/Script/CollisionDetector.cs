using Effects;
using UnityEngine;

namespace Player
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private RunnerLauncher _runnerLauncher;
        [SerializeField] private SideMovement _sideMovement;
        [SerializeField] private Shaker _shaker;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _maxDistanceToGameOver = 1.2f;
        private int _touchCount = 0;

        private void OnTriggerEnter(Collider collider)
        {
            DetermineTypeCollision(collider);
        }

        private void DetermineTypeCollision(Collider collider)
        {
            if ((_layer.value & (1 << collider.transform.gameObject.layer)) > 0)
            {
                float obstaclePosition = collider.transform.position.x;
                if (IsFullCollision(obstaclePosition) || _touchCount + 1 > 1)
                    _runnerLauncher.StopRun();
                else
                {
                    _sideMovement.AbortMoving();
                    _touchCount++;
                }
                _shaker.StartShake();
            }
        }

        private bool IsFullCollision(float obstaclePosition)
        {
            return Mathf.Abs(obstaclePosition - transform.position.x) < _maxDistanceToGameOver;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private GameLauncher _gameLauncher;
        [SerializeField] private SideMovement _sideMovement;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float maxDistanceToGameOver = 1.2f;

        private int _touchCount = 0;

        private void OnTriggerEnter(Collider collider)
        {
            if ((_layer.value & (1 << collider.transform.gameObject.layer)) > 0)
            {
                float obstaclePosition = collider.transform.position.x;
                if (Mathf.Abs(obstaclePosition - transform.position.x) < maxDistanceToGameOver || _touchCount + 1 > 1)
                {
                    _gameLauncher.StopGame();
                }
                else
                {
                    _sideMovement.AbortMoving();
                    _touchCount++;
                    DebugText.Show(_touchCount.ToString());
                }
            }
        }
    }
}

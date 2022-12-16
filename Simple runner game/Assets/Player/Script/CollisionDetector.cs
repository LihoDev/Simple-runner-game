using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private GameLauncher _gameLauncher;
        [SerializeField] private LayerMask _layer;

        private void OnTriggerEnter(Collider other)
        {
            if ((_layer.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                _gameLauncher.StopGame();
            }
        }
    }
}

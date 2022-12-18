using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Player
{
    public class CollectCoins : MonoBehaviour
    {
        public int Count { get; private set; } = 0;

        [SerializeField] private TMP_Text _text;
        [SerializeField] private LayerMask _layer;

        private void OnTriggerEnter(Collider collider)
        {
            if ((_layer.value & (1 << collider.transform.gameObject.layer)) > 0)
            {
                Count++;
                _text.text = Count.ToString();
                collider.gameObject.SetActive(false);
            }
        }
    }
}
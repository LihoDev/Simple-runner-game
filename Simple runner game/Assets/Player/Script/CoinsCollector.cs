using Prop;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class CoinsCollector : MonoBehaviour
    {
        public int Count { get; private set; } = 0;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private UnityEvent Collect;

        private void OnTriggerEnter(Collider collider)
        {
            CollectCoin(collider);
        }

        private void CollectCoin(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out Coin coin))
            {
                Count++;
                _text.text = Count.ToString();
                collider.gameObject.SetActive(false);
                Collect?.Invoke();
            }
        }
    }
}
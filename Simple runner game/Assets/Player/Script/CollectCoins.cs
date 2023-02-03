using Prop;
using TMPro;
using UnityEngine;

namespace Player
{
    public class CollectCoins : MonoBehaviour
    {
        public int Count { get; private set; } = 0;
        [SerializeField] private TMP_Text _text;

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
            }
        }
    }
}
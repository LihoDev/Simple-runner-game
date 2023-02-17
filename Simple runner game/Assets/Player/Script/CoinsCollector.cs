using Prop;
using TMPro;
using UnityEngine;

namespace Player
{
    public class CoinsCollector : Collector
    {
        public int Count { get; private set; } = 0;
        [SerializeField] private TMP_Text _text;

        protected override void Collect(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out Coin coin))
            {
                Count++;
                _text.text = Count.ToString();
                collider.gameObject.SetActive(false);
                base.Collect(collider);
            }
        }
    }
}
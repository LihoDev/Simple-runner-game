using TMPro;
using UnityEngine;

namespace Ui
{
    public class Score : MonoBehaviour
    {
        public int Value { get => (int)_value; }
        [SerializeField] private TMP_Text _text;
        private float _value;

        private void Update()
        {
            IncreaseValue();
        }

        private void IncreaseValue()
        {
            _value += Time.deltaTime;
            _text.text = Value.ToString();
        }
    }
}
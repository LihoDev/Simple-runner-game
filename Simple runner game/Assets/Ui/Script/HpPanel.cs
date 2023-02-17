using System.Collections.Generic;
using UnityEngine;

namespace Ui
{
    public class HpPanel : MonoBehaviour
    {
        [SerializeField] private Transform _hpIconPrefab;
        private List<UiScaleShaker> _hpIcons = new List<UiScaleShaker>();
        private int _activeUnit;

        public void InstantiateHpIcons(int count)
        {
            if (_hpIcons.Count == 0)
            {
                for (var i = 0; i < count; i++)
                {
                    Transform instance = Instantiate(_hpIconPrefab, Vector3.zero, Quaternion.identity, transform);
                    if (instance.TryGetComponent(out UiScaleShaker icon))
                        _hpIcons.Add(icon);
                    else
                        Debug.LogError("UiScaleShaker not found");
                }
            }
            else
                Debug.LogWarning("Hp icons already instantiated");
            _activeUnit = count - 1;
        }

        public void RemoveOne()
        {
            if (_activeUnit >= 0)
            {
                _hpIcons[_activeUnit].gameObject.SetActive(false);
                _activeUnit--;
            }
            if (_activeUnit >= 0)
                _hpIcons[_activeUnit].ShakeScale();
        }

        public void AddOne()
        {
            if (_activeUnit + 1 < _hpIcons.Count)
            {
                _activeUnit++;
                _hpIcons[_activeUnit].gameObject.SetActive(true);
                _hpIcons[_activeUnit].ShakeScale();
            }
        }
    }
}
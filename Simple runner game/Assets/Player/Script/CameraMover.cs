using System.Collections;
using UnityEngine;

namespace Player
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed = 0;

        public void MoveCamera()
        {
            StartCoroutine(AnimateMoving());
        }

        private IEnumerator AnimateMoving()
        {
            while (Vector3.Distance(transform.localPosition, _target.localPosition) > 0.01)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, _target.localPosition, _speed * Time.deltaTime);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, _target.localRotation, _speed * Time.deltaTime);
                yield return null;
            }
            transform.localPosition = _target.localPosition;
            transform.localRotation = _target.localRotation;
        }
    }
}
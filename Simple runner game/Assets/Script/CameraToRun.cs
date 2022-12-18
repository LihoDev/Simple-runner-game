using System.Collections;
using UnityEngine;

public class CameraToRun : MonoBehaviour
{
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _speed = 0;

    public void MoveCamera()
    {
        StartCoroutine(AnimateMoving());
    }

    private IEnumerator AnimateMoving()
    {
        while(Vector3.Distance(transform.localPosition, _cameraTarget.localPosition)> 0.01)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _cameraTarget.localPosition, _speed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _cameraTarget.localRotation, _speed * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = _cameraTarget.localPosition;
        transform.localRotation = _cameraTarget.localRotation;
    }
}

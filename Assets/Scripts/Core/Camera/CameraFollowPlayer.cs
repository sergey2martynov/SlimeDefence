using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _offsetPosition;

    private void Start()
    {
        _offsetPosition = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        transform.position = _target.transform.position + _offsetPosition;
    }
}

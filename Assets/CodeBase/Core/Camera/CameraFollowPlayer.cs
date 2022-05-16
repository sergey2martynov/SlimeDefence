using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Character _characterController;

    private Vector3 _offsetPosition = new Vector3(0, 80, 50);


    private void LateUpdate()
    {
        transform.position = _characterController.transform.position + _offsetPosition;
    }
}

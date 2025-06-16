using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SecretLevelEntrance : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameSetup collidingGameSetup = other.GetComponentInParent<GameSetup>();
        if (other.CompareTag("Player") && collidingGameSetup._keys >= 3)
        {
            if (_targetObject)
            {
                // Get position delta
                Vector3 positionDelta = _targetObject.transform.position - other.transform.position;

                // Teleport the player
                other.transform.position = _targetObject.transform.position;

                // Teleport the camera (without shaking)
                CinemachineVirtualCameraBase virtualCamera = CinemachineCore.GetVirtualCamera(0);
                virtualCamera.OnTargetObjectWarped(other.transform, positionDelta);
            }
        }
    }
}

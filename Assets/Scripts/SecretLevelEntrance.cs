using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SecretLevelEntrance : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetObject;
    
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable collidingDamageable = other.GetComponentInParent<Damageable>();
        if (other.CompareTag("Player") && collidingDamageable._keys >= 3)
        {
            if(_targetObject)
                other.transform.position = _targetObject.transform.position;
        }
    }

}

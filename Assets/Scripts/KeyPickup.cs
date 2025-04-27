using UnityEngine;
using UnityEngine.Serialization;

public class KeyPickup : MonoBehaviour
{
    [FormerlySerializedAs("keys")] [SerializeField]
    public int addKey = 1;
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            Damageable collidingDamageable = other.GetComponentInParent<Damageable>();
            
            collidingDamageable.AddKey();
            if (collidingDamageable._keys <= 1)
            {
                Debug.Log("Key pickup 1");
                collidingDamageable.keysContainer[0].SetActive(true);
            }
            else if(collidingDamageable._keys == 2){
                Debug.Log("Key pickup 2");
                collidingDamageable.keysContainer[1].SetActive(true); 
            } else if (collidingDamageable._keys == 3)
            {
                Debug.Log("Key pickup 3");
                collidingDamageable.keysContainer[2].SetActive(true); 

            }
                Destroy(this.gameObject);
                
        }
        

    }
}

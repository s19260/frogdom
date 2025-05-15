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
            
            GameSetup collidingGameSetup = other.GetComponentInParent<GameSetup>();
            
            collidingGameSetup.AddKey();
            if (collidingGameSetup._keys <= 1)
            {
                Debug.Log("Key pickup 1");
                collidingGameSetup.keysContainer[0].SetActive(true);
            }
            else if(collidingGameSetup._keys == 2){
                Debug.Log("Key pickup 2");
                collidingGameSetup.keysContainer[1].SetActive(true); 
            } else if (collidingGameSetup._keys == 3)
            {
                Debug.Log("Key pickup 3");
                collidingGameSetup.keysContainer[2].SetActive(true); 

            }
                Destroy(this.gameObject);
                
        }
        

    }
}

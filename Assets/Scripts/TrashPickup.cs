using Unity.VisualScripting;
using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trash Pickup");
            GameSetup collidingGameSetup = other.GetComponentInParent<GameSetup>();
           
            collidingGameSetup.AddTrash();
            
        }
        Destroy(this.gameObject);
    }
}

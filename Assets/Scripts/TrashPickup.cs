using Unity.VisualScripting;
using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trash Pickup");
            FindObjectOfType<ProgressBar>().Collect();
            Destroy(gameObject);
            
        }

    }
}

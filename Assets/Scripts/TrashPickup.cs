using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
//            Debug.Log("Trash Pickup");
            // Pass the player's transform to the Collect method
            FindObjectOfType<ProgressBar>().Collect(other.transform);
            Destroy(gameObject);
        }
    }
}
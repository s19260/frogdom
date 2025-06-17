using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    public AudioClip pickupSound; 
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
//            Debug.Log("Trash Pickup");
            // Pass the player's transform to the Collect method
            PlayPickupSound();
            FindObjectOfType<ProgressBar>().Collect(other.transform);
            GetComponent<SpriteRenderer>().enabled = false;
             Destroy(gameObject, pickupSound.length);
        }
    }
    
    private void PlayPickupSound()
    {
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }

}
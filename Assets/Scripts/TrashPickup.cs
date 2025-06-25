using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            FindObjectOfType<ProgressBar>().Collect(other.transform);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
        }
    }
}
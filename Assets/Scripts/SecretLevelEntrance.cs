using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SecretLevelEntrance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("Secret Level Entrance " +other.transform.position);
        {
            SceneManager.LoadScene("SecretLevel1");
        }
    }

}

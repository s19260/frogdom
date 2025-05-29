using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level ended");
            
            // Get the PlayerInputSender from the PLAYER (not the trigger object)
            PlayerInputSender inputSender = other.GetComponent<PlayerInputSender>();
            if (inputSender != null)
            {
                inputSender.OnLevelComplete();
            }
            else
            {
                Debug.LogError("PlayerInputSender not found on player!");
            }
            
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
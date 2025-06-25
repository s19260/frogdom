using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level ended");
            
            PlayerInputSender inputSender = other.GetComponent<PlayerInputSender>();
            if (inputSender != null)
            {
                inputSender.OnLevelComplete();
            }
            else
            {
                Debug.LogError("PlayerInputSender not found on player!");
            }

            if (SceneManager.GetActiveScene().name == "MainMenuScene")
            {
                SceneManager.LoadScene("Tutorial");
            }

            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                SceneManager.LoadScene("Level 1");
            }
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                SceneManager.LoadScene("Level 2");
            }

            if (SceneManager.GetActiveScene().name == "Level 2")
            {
                SceneManager.LoadScene("Level 3");
            }

            if (SceneManager.GetActiveScene().name == "Level 3")
            {
                SceneManager.LoadScene("Level 1B");
            }

            if (SceneManager.GetActiveScene().name == "Level 1B")
            {
                SceneManager.LoadScene("Level 2B");
            }

            if (SceneManager.GetActiveScene().name == "Level 2B")
            {
                SceneManager.LoadScene("Level 3B");
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (other.CompareTag("Player") && sceneName == "GameplayScene")
        {
            Debug.Log("Level 1 ended");
            PlayerInputSender inputSender = other.GetComponent<PlayerInputSender>();
            if (inputSender != null)
            {
                inputSender.OnLevelComplete();
            }
            else
            {
                Debug.LogError("PlayerInputSender not found on player!");
            }
            SceneManager.LoadScene("Level 2");
        }
        else if (other.CompareTag("Player") && sceneName == "Level 2")
        {
            Debug.Log("Level 2 ended");
            PlayerInputSender inputSender = other.GetComponent<PlayerInputSender>();
            if (inputSender != null)
            {
                inputSender.OnLevelComplete();
            }
            else
            {
                Debug.LogError("PlayerInputSender not found on player!");
            }
            SceneManager.LoadScene("Level 3");

        }
    }

}
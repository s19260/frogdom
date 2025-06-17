using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInputSender inputSender = other.GetComponent<PlayerInputSender>();
            if (inputSender != null)
            {
                inputSender.OnLevelComplete();
            }   
            Debug.Log("Game Over");
            SceneManager.LoadScene("MainMenuScene");
        }
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameplayScene");
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUIManager : MonoBehaviour
{
    public GameObject panel; // Assign the JumpPowerUpPanel in inspector
    public Text infoText;    // Assign the Text component in inspector

    private bool waitingForInput = false;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void Show(string message)
    {
        panel.SetActive(true);
        infoText.text = message;
        Time.timeScale = 0f; // Freeze the game
        waitingForInput = true;
    }

    private void Update()
    {
        if (waitingForInput && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            panel.SetActive(false);
            Time.timeScale = 1f; // Unfreeze the game
            waitingForInput = false;
        }
    }
}
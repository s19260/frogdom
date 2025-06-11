using UnityEngine;
using UnityEngine.UI;

public class PowerUpUIManager : MonoBehaviour
{
    public GameObject panel;
    public Text infoText;
    private bool waitingForInput = false;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void Show(PowerUpType powerUpType)
    {
        panel.SetActive(true);
        infoText.text = GetPowerUpMessage(powerUpType);
        Time.timeScale = 0f;
        waitingForInput = true;
    }

    private string GetPowerUpMessage(PowerUpType type)
    {
        return type switch
        {
            PowerUpType.Jump => "Jump Power-Up Acquired!\nPress Space/Enter to continue",
            PowerUpType.Attack => "Attack Power-Up Acquired!\nPress Space/Enter to continue",
            PowerUpType.Dash => "Dash Power-Up Acquired!\nPress Space/Enter to continue",
            _ => "Power-Up Collected!"
        };
    }

    private void Update()
    {
        if (waitingForInput && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
            waitingForInput = false;
        }
    }
}

public enum PowerUpType
{
    Jump,
    Attack,
    Dash
}
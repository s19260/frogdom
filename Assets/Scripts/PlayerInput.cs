using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerInputSender : MonoBehaviour
{
    // Set this to your local or remote PHP endpoint
    [Tooltip("URL to your PHP insert_input.php script")]
    public string phpUrl = "http://127.0.0.1:8000/insert_input.php";

    // Call this method to send input data
    public void SendPlayerInput(string inputType, string inputValue)
    {
        StartCoroutine(SendInputCoroutine(inputType, inputValue));
    }

    private IEnumerator SendInputCoroutine(string inputType, string inputValue)
    {
        WWWForm form = new WWWForm();
        form.AddField("input_type", inputType);
        form.AddField("input_value", inputValue);

        using (UnityWebRequest www = UnityWebRequest.Post(phpUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Input sent! Server response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error sending input: " + www.error);
            }
        }
    }

    // Example: Send input when the spacebar is pressed

    void Update()
    {
        // Example: Log every key pressed
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                SendPlayerInput(kcode.ToString(), "Key Pressed");
            }
        }
    }

}
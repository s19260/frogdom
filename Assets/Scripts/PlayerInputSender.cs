using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerInputSender : MonoBehaviour
{
    [Tooltip("URL to your PHP script handling both tables")]
    public string phpUrl = "http://127.0.0.1:8000/insert_data.php";

    // For general inputs
    public void SendPlayerInput(string inputType, string inputValue)
    {
        StartCoroutine(SendInputCoroutine(inputType, inputValue));
    }

    // For death counts, now with scene name
    public void SendDeathCount(int deathCount)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(SendDeathCountCoroutine(deathCount, sceneName));
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
                Debug.Log("Input sent! Response: " + www.downloadHandler.text);
            else
                Debug.LogError("Error sending input: " + www.error);
        }
    }

    private IEnumerator SendDeathCountCoroutine(int deathCount, string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("death_count", deathCount.ToString());
        form.AddField("scene_name", sceneName);

        using (UnityWebRequest www = UnityWebRequest.Post(phpUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log("Death count sent! Response: " + www.downloadHandler.text);
            else
                Debug.LogError("Error sending death count: " + www.error);
        }
    }
}
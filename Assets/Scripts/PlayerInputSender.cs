using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerInputSender : MonoBehaviour
{
    [Tooltip("URL to your PHP script handling all tables")]
    public string phpUrl = "http://127.0.0.1:8000/insert_input.php";

    private float levelStartTime;

    void Start()
    {
        levelStartTime = Time.time;
    }

    // Call this when the player completes the level
    public void OnLevelComplete()
    {
        float completionTime = Time.time - levelStartTime;
        string sceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(SendCompletionTime(sceneName, completionTime));
    }

    private IEnumerator SendCompletionTime(string sceneName, float completionTime)
    {
        WWWForm form = new WWWForm();
        form.AddField("scene_name", sceneName);
        form.AddField("completion_time", completionTime.ToString("F2"));

        using (UnityWebRequest www = UnityWebRequest.Post(phpUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log("Completion time sent! Response: " + www.downloadHandler.text);
            else
                Debug.LogError("Error sending completion time: " + www.error);
        }
    }
    // For general inputs
    public void SendPlayerInput(string inputType, string inputValue)
    {
        StartCoroutine(SendInputCoroutine(inputType, inputValue));
    }
    void Update()
    {
        // Example: Log every key press (modify as needed)
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                SendPlayerInput("KeyPress", kcode.ToString());
            }
        }
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
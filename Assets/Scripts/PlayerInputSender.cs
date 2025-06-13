using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerInputSender : MonoBehaviour
{
    [Tooltip("URL to your PHP script handling all tables")]
    public string phpUrl = "http://127.0.0.1:8000/insert_input.php";

    private float levelStartTime;
    private int currentUserID; 

    void Start()
    {
        levelStartTime = Time.time;
        currentUserID = PlayerPrefs.GetInt("UserID", -1); 
    }
    
    public void OnLevelComplete()
    {
        float completionTime = Time.time - levelStartTime;
        string sceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(SendCompletionTime(sceneName, completionTime));
    }

    private IEnumerator SendCompletionTime(string sceneName, float completionTime)
    {
        if (currentUserID == -1) yield break; 

        WWWForm form = new WWWForm();
        form.AddField("user_id", currentUserID); 
        form.AddField("scene_name", sceneName);
        form.AddField("completion_time", completionTime.ToString("F2"));

        using (UnityWebRequest www = UnityWebRequest.Post(phpUrl, form))
        {
            yield return www.SendWebRequest();
            Debug.Log("Response: " + www.downloadHandler.text); 
        }
    }
    // For general inputs
    public void SendPlayerInput(string inputType, string inputValue)
    {
        StartCoroutine(SendInputCoroutine(inputType, inputValue));
    }
    void Update()
    {
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                SendPlayerInput("KeyPress", kcode.ToString());
            }
        }
    }

    public void SendDeathCount(int deathCount)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(SendDeathCountCoroutine(deathCount, sceneName));
    }

    private IEnumerator SendInputCoroutine(string inputType, string inputValue)
    {
        if (currentUserID == -1) yield break;

        WWWForm form = new WWWForm();
        form.AddField("user_id", currentUserID); 
        form.AddField("input_type", inputType);
        form.AddField("input_value", inputValue);

        using (UnityWebRequest www = UnityWebRequest.Post(phpUrl, form))
        {
            yield return www.SendWebRequest();
            Debug.Log("Input response: " + www.downloadHandler.text);
        }
    }

    private IEnumerator SendDeathCountCoroutine(int deathCount, string sceneName)
    {
        if (currentUserID == -1) yield break;

        WWWForm form = new WWWForm();
        form.AddField("user_id", currentUserID); 
        form.AddField("death_count", deathCount.ToString());
        form.AddField("scene_name", sceneName);

        using (UnityWebRequest www = UnityWebRequest.Post(phpUrl, form))
        {
            yield return www.SendWebRequest();
            Debug.Log("Death response: " + www.downloadHandler.text);
        }
    }
}

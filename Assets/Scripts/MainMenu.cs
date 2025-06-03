using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public InputField usernameInput;
    public GameObject errorText;
    private string apiURL = "http://127.0.0.1:8000/insert_input.php";

    public void PlayGame()
    {
        StartCoroutine(SendUsername());
    }

    IEnumerator SendUsername()
    {
        if (string.IsNullOrWhiteSpace(usernameInput.text))
        {
            errorText.SetActive(true);
            errorText.GetComponent<Text>().text = "Enter a username!";
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("username", usernameInput.text);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(apiURL, form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Parse JSON response
                var response = JsonUtility.FromJson<UserResponse>(webRequest.downloadHandler.text);
                
                if (response.status == "success")
                {
                    // Store the received user ID
                    PlayerPrefs.SetInt("UserID", response.user_id);
                    Debug.Log("Stored UserID: " + response.user_id);
                    
                    SceneManager.LoadScene("GameplayScene");
                    Cursor.visible = false;
                }
                else
                {
                    errorText.SetActive(true);
                    errorText.GetComponent<Text>().text = response.message;
                }
            }
            else
            {
                errorText.SetActive(true);
                errorText.GetComponent<Text>().text = "Connection failed: " + webRequest.error;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    [System.Serializable]
    private class UserResponse
    {
        public string status;
        public string message;
        public int user_id;  // This must match the PHP response field name
    }
}

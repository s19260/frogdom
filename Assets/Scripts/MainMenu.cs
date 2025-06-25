using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public InputField usernameInput;
    public GameObject errorText;
    public GameObject creditsPanel; 
    public Button creditsButton;    
    public Button backButton;       
    public Text creditsText;        
    private string apiURL = "https://serfer.izoslav.pl";

    private void Start()
    {
        creditsPanel.SetActive(false);
        backButton.gameObject.SetActive(false);
        
        creditsButton.onClick.AddListener(ShowCredits);
        backButton.onClick.AddListener(BackToMainMenu);
    }

    public void ShowCredits()
    {
        usernameInput.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        errorText.SetActive(false);
        
        creditsPanel.SetActive(true);
        backButton.gameObject.SetActive(true);
        
        creditsText.text = @"
Creators
Artist: Aysegul Deger,  Programmer: Hubert Wisniewski
";
    }
    

    public void BackToMainMenu()
    {
        usernameInput.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        
        creditsPanel.SetActive(false);
        backButton.gameObject.SetActive(false);
    }
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
                var response = JsonUtility.FromJson<UserResponse>(webRequest.downloadHandler.text);
                
                if (response.status == "success")
                {
                    PlayerPrefs.SetInt("UserID", response.user_id);
                    Debug.Log("Stored UserID: " + response.user_id);
                    
                    SceneManager.LoadScene("Tutorial");
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
        public int user_id;  
    }
}

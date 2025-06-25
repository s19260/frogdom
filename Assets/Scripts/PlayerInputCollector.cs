using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PlayerInputCollector : MonoBehaviour
{
    public InputField inputField; 
    private string filePath;

    void Start()
    {
        filePath =  "/Users/hubertwisniewski/Documents/GitHub/frogdom/playerInputs.txt";
    }

    void Update()
    {
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                WriteToFile("Key Pressed: " + kcode.ToString());
            }
        }
    }

    public void OnInputFieldSubmit(string userInput)
    {
        WriteToFile("InputField: " + userInput);
    }

    private void WriteToFile(string text)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + text);
        }
    }
}
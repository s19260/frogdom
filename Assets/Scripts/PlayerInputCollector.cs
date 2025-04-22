using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PlayerInputCollector : MonoBehaviour
{
    public InputField inputField; // Assign this in the Inspector if using UI input

    private string filePath;

    void Start()
    {
        filePath =  "/Users/hubertwisniewski/Documents/GitHub/frogdom/playerInputs.txt";
    }

    void Update()
    {
        // Example: Log every key pressed
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                WriteToFile("Key Pressed: " + kcode.ToString());
            }
        }
    }

    // Call this from the InputField's OnEndEdit event
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
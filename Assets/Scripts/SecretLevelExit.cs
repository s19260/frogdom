using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SecretLevelExit : MonoBehaviour
{

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
        
    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
        
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        scene = SceneManager.GetSceneByName("GameplayScene");
        mode = LoadSceneMode.Single;
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            
        if (other.CompareTag("Player"))
        {
            CheckpointController checkpointController = FindObjectOfType<CheckpointController>();
            SceneManager.LoadScene("GameplayScene");
            Debug.Log("Secret Level Exit " + other.transform.position);
            other.transform.position = checkpointController._startingPosition;
            
        }
    }
    
}

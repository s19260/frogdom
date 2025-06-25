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
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
        
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
        
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        scene = SceneManager.GetSceneByName("Level 1");
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
            SceneManager.LoadScene("Level 1");
            Debug.Log("Secret Level Exit " + other.transform.position);
            other.transform.position = checkpointController._startingPosition;
        }
    }
}

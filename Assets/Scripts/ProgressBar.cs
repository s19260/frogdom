using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider progressBar;          
    public int totalCollectibles = 20;  
    private int collected = 0;          
    public GameObject jumpPowerUpPrefab; // Add this field for the power-up prefab

    void Start()
    {
        progressBar.interactable = false; 
        progressBar.minValue = 0;
        progressBar.maxValue = totalCollectibles;
        progressBar.value = 0;
    }

    public void Collect(Transform playerTransform) // Add player transform parameter
    {
        collected++;
        progressBar.value = collected;

        if (collected >= totalCollectibles)
        {
            collected = 0;
            progressBar.value = collected;
            
            // Spawn power-up near player
            Vector3 spawnPosition = playerTransform.position + new Vector3(1, 0, 0);
            Instantiate(jumpPowerUpPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
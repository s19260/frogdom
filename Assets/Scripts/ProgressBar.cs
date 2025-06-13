using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider progressBar;
    public int totalCollectibles = 2;
    private int collected = 0;
    
    [Header("Power-Up Prefabs")]
    public GameObject jumpPowerUpPrefab;
    public GameObject attackPowerUpPrefab;
    public GameObject dashPowerUpPrefab;

    [Header("References")]
    public PowerUpUIManager powerUpUIManager;

    private PowerUpType nextPowerUp = PowerUpType.Jump;
    private readonly PowerUpType[] powerUpCycle = new PowerUpType[]
    {
        PowerUpType.Jump,
        PowerUpType.Attack,
        PowerUpType.Dash
    };
    private int cycleIndex = 0;

    void Start()
    {
        progressBar.interactable = false;
        progressBar.minValue = 0;
        progressBar.maxValue = totalCollectibles;
        progressBar.value = 0;
        if (SceneManager.GetActiveScene().name == "level 2")
        {
            nextPowerUp = PowerUpType.Attack;
        }
        else if (SceneManager.GetActiveScene().name == "level 3")
        {
            nextPowerUp = PowerUpType.Dash;
        }
    }

    public void Collect(Transform playerTransform)
    {
        collected++;
        progressBar.value = collected;

        if (collected >= totalCollectibles)
        {
            collected = 0;
            progressBar.value = collected;
            
            SpawnPowerUp(playerTransform);
            CycleNextPowerUp();
        }
    }

    private void SpawnPowerUp(Transform playerTransform)
    {
        Vector3 spawnPosition = playerTransform.position + new Vector3(1, 0, 0);
        GameObject prefabToSpawn = nextPowerUp switch
        {
            PowerUpType.Jump => jumpPowerUpPrefab,
            PowerUpType.Attack => attackPowerUpPrefab,
            PowerUpType.Dash => dashPowerUpPrefab,
            _ => jumpPowerUpPrefab
        };

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        powerUpUIManager.Show(nextPowerUp);
    }

    private void CycleNextPowerUp()
    {

            cycleIndex = (cycleIndex + 2) % powerUpCycle.Length;
            nextPowerUp = powerUpCycle[cycleIndex];
        
    }
}
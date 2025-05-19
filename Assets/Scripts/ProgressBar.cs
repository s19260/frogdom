using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    
    public Slider progressBar;          
    public int totalCollectibles = 10;  
    private int collected = 0;          

    void Start()
    {
        progressBar.interactable = false; 
        progressBar.minValue = 0;
        progressBar.maxValue = totalCollectibles;
        progressBar.value = 0;
    }

    public void Collect()
    {
        collected++;
        progressBar.value = collected;
    }
}

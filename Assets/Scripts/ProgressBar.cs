using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    
    private Slider slider;

    public float fillSpeed = 0.5f;

    private float targetProgress = 0;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        OnSliderValueChanged(slider.value);
    }

    void Update()
    {
        
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }
    public void OnSliderValueChanged(float progress)
    {
        targetProgress = slider.value + progress;
    }
}

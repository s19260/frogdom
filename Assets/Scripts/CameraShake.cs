using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 _originalPosition;
    private float _shakeDuration = 0f;
    private float _shakeIntensity = 0.1f;

    void Start()
    {
        _originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (_shakeDuration > 0)
        {
            transform.localPosition = _originalPosition + Random.insideUnitSphere * _shakeIntensity;
            _shakeDuration -= Time.deltaTime;
        }
        else
        {
            _shakeDuration = 0f;
            transform.localPosition = _originalPosition;
        }
    }

    public void Shake(float duration, float intensity)
    {
        _shakeDuration = duration;
        _shakeIntensity = intensity;
    }
}
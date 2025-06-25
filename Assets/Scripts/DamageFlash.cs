using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.15f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.3f);

    private Coroutine flashCoroutine;

    private void Awake()
    {
        if (flashImage != null)
            flashImage.color = new Color(1, 0, 0, 0);
    }

    public void Flash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        if (flashImage == null) yield break;
        flashImage.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        flashImage.color = new Color(1, 0, 0, 0); 
    }
}
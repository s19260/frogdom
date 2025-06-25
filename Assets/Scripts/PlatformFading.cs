using System.Collections;
using UnityEngine;

public class PlatformFading : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float originalAlpha;
    private Coroutine currentCoroutine;
    private bool hasPlayerTouched = false;
    private Collider2D collider2d;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalAlpha = spriteRenderer.color.a;
        collider2d = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!hasPlayerTouched)
            {
                StartCoroutine(FadeOut());
                hasPlayerTouched = true;
            }
        }
    }


    private IEnumerator FadeOut()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        
        while(spriteRenderer.color.a > 0)
        {
            var color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, color.a - 0.01f);
            yield return new WaitForSeconds(Time.unscaledDeltaTime * 5);
        }

        collider2d.enabled = false;
        yield return new WaitForSeconds(3);
        currentCoroutine = StartCoroutine(FadeIn());
        collider2d.enabled = true;
    }

    private IEnumerator FadeIn()
    {
        hasPlayerTouched = false;
        while (spriteRenderer.color.a < originalAlpha)
        {
            var color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, color.a + 0.01f);
            yield return new WaitForSeconds(Time.unscaledDeltaTime);
        }
        currentCoroutine = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0;
    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color originalColor;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;
        float newApha = originalColor.a * (1 - (timeElapsed / fadeTime));
        
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, newApha);
        if (timeElapsed > fadeTime)
        {
            objToRemove.SetActive(false);
            
        }
    }
        
}

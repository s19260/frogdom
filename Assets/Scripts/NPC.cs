using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NPC : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;

    public float wordSpeed;
    public bool playerIsClose;

    private Coroutine typingCoroutine;

    void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && playerIsClose)
        {
            if (IsDialogueBoxEnabled())
            {
                DisableDialogueBox();
            }
            else
            {
                EnableDialogueBox();
            }
        }
    }

    public void EnableDialogueBox()
    {
        dialogueBox.SetActive(true);
        typingCoroutine = StartCoroutine(Typing());
    }

    public void DisableDialogueBox()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text = "";
		dialogueBox.SetActive(false);
    }

    public bool IsDialogueBoxEnabled()
    {
        return dialogueBox.activeInHierarchy;
    }

    IEnumerator Typing()
    {
        dialogueText.text = "";
        foreach (char letter in dialogue[0].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            DisableDialogueBox();
        }
    }
}

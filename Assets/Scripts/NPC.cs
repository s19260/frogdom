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
    private int index;

    public float wordSpeed;
    public bool playerIsClose;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialogueBox.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialogueBox.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0; 
        dialogueBox.SetActive(false);
        
    }
    void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player is close");
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}

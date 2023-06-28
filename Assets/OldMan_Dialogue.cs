using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OldMan_Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;
    public Animator animator;
    public float movementSpeed = 3f;
    public float wordSpeed;
    public bool playerIsClose = false;
    private bool isTyping = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && playerIsClose)
        {
            if (!isTyping) // Check if text is not currently being typed
            {
                NextLine();
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    private IEnumerator Typing()
    {
        isTyping = true; // Set typing flag to true
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isTyping = false; // Set typing flag to false once text is finished typing

        if (index == dialogue.Length - 1)
        {
            StartMoving(); // Call the method to make the NPC run away after the last line of dialogue
        }
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
            ZeroText();
    }

    private void StartMoving()
    {
        animator.SetBool("IsMoving", true);
        StartCoroutine(MoveNPC());
    }

    private IEnumerator MoveNPC()
    {
        float distance = 0f;
        while (distance < 100f) // Adjust the distance as per your needs
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            distance += movementSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
}

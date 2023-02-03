using System.Collections;
using UnityEngine;
using TMPro;

public class ControlDialogue : MonoBehaviour
{
    [SerializeField] private GameObject InRangeSprite;
    [SerializeField, TextArea(1,5)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float textSpeed= 0.05f;

    private bool DialogueActive=false;
    private int index;
    private bool playerInRange = false;




    public void Update()
    {

        if (playerInRange == true && Input.GetKeyDown(KeyCode.F))
        {
            if (DialogueActive==false) 
            {
                StartDialogue();
            }    
            else if (dialogueText.text == dialogueLines[index]) 
            {
                NextDialogue();
            }
            else 
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[index];
            }
        }
    }

    private void StartDialogue() 
    {
        Time.timeScale = 0f;
        DialogueActive = true;
        dialoguePanel.SetActive(true);
        InRangeSprite.SetActive(false);
        index = 0;
        StartCoroutine(ShoText());
    }

    private void NextDialogue() 
    {
        index++;
        if(index< dialogueLines.Length) 
        {
            StartCoroutine(ShoText());
        }
        else 
        {
            DialogueActive = false;
            dialoguePanel.SetActive(false);
            InRangeSprite.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShoText()
    {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[index]) 
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "PlayerBase")
        {
            playerInRange = true;
            InRangeSprite.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "PlayerBase")
        {
            playerInRange = false;
            InRangeSprite.SetActive(false);
        }
    }
}

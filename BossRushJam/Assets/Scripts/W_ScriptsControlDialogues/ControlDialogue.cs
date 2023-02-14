using System.Collections;
using UnityEngine;
using TMPro;

public class ControlDialogue : MonoBehaviour
{
    [SerializeField] private GameObject InRangeSprite;
    [SerializeField] private GameObject intructionText;
    [SerializeField] private GameObject nameNpcPanel;
    [SerializeField] private TMP_Text nameNpcText;
    [SerializeField, TextArea(1,5)] private string[] dialogueLines;
    [SerializeField] private string nameNpc;
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
        nameNpcPanel.SetActive(true);
        InRangeSprite.SetActive(false);
        intructionText.SetActive(false);
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
            nameNpcPanel.SetActive(false);
            InRangeSprite.SetActive(true);
            intructionText.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShoText()
    {
        dialogueText.text = string.Empty;
        nameNpcText.text = string.Empty ;
        nameNpcText.text = nameNpc ;

        foreach(char ch in dialogueLines[index]) 
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playerInRange = true;
            InRangeSprite.SetActive(true);
            intructionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playerInRange = false;
            InRangeSprite.SetActive(false);
            intructionText.SetActive(false);
        }
    }
}

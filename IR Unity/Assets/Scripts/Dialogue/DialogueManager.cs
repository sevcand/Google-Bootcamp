using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Sahnedeki tüm diyalogları bu script kontrol edecek.

    private Queue<string> sentences;

    [Header(" -- DIALOGUE -- ")]
    public TMP_Text npcNameText;
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    
    private void Start()
    {
        //initializing
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
    }

    public void StartDialogue(DialogueTemplate dialogue)
    {
        Debug.Log(dialogue.npcName + " ile diyalog başladı!");

        dialogueBox.SetActive(true);

        npcNameText.SetText(dialogue.npcName);

        sentences.Clear(); // kuyruğu temizle

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string currentSentence = sentences.Dequeue();
        Debug.Log(currentSentence);

        dialogueText.SetText(currentSentence);
    }

    void EndDialogue()
    {
        Debug.Log("Diyalog sonlandı!");
        dialogueBox.SetActive(false);
    }
}

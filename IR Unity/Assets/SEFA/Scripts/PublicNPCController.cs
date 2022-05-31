using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PublicNPCController : MonoBehaviour
{
    // Trigger alanına girince rastgele diyalog çıkacak
    // çıkan cümle bir kaç saniye sonra kaybolsun
    // kaybolana kadar yenisi çıkmasın

    public GameObject popUpCanvas;
    public PublicDialogueSet _dialogueSet;
    public TMP_Text textUI;
    private bool isReadyForNewSentence;
    [Header("-- Settings --")]
    public int _dialogueShowingTime = 5;
    public int _newSentenceReadyTime = 10;

    private void Start()
    {
        popUpCanvas.SetActive(false);
        isReadyForNewSentence = true;
        Debug.Log("NPC Cümle sayısı: " + _dialogueSet.sentences.Length);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // listeden rastgele bir cümle seç ve göster.
            int DialogueVisibleRandom = Random.Range(0, 2);

            if (DialogueVisibleRandom == 1 && isReadyForNewSentence)
            {
                string sentence = RandomSentence();
                ShowDialogue(sentence);
            }
        }
    }

    void ShowDialogue(string sentence)
    {
        popUpCanvas.SetActive(true);
        textUI.SetText(sentence);

        DialogueProcess();
    }

    void DialogueProcess()
    {
        // diyalog gösterilmeye başlandıktan sonraki süreç
        // 5 sn gösterilsin, 10 sn sonra hazır hale gelsin
        isReadyForNewSentence = false;

        Invoke("HideDialogue", _dialogueShowingTime);
        Invoke("ReadyForNewSentence", _newSentenceReadyTime);

    }

    string RandomSentence()
    {

        int listLenght = _dialogueSet.sentences.Length;
        int randomNumber = Random.Range(0, listLenght);
       
        return _dialogueSet.sentences[randomNumber];
    }

    void ReadyForNewSentence()
    {
        Debug.Log("ready fonksiyonu çalıştı");
        isReadyForNewSentence = true;
    }

    void HideDialogue()
    {
        Debug.Log("hide dialogue çalıştı");
        popUpCanvas.SetActive(false);
    }

}

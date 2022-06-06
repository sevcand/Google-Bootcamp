using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Sahnedeki tüm diyalogları bu script kontrol edecek.

    private Queue<string> sentences;

    [Header(" -- DIALOGUE -- ")]
    public TMP_Text npcNameText;
    public TMP_Text dialogueText;
    public GameObject dialogueCanvas;

    [Header(" -- ANSWER BUTTONS -- ")]
    public Button answerFirst;
    public Button answerSecond;
    public Button answerThird;

    private Answer[] mDialogueAnswers;

    private void Start()
    {
        //initializing
        sentences = new Queue<string>();
        dialogueCanvas.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        mDialogueAnswers = dialogue.answers;

        Debug.Log("cevap sayısı :" + mDialogueAnswers.Length);
        Debug.Log(dialogue.npcName + " ile diyalog başladı!");

        answerFirst.gameObject.SetActive(false);
        answerSecond.gameObject.SetActive(false);
        answerThird.gameObject.SetActive(false);
        dialogueCanvas.SetActive(true);

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
        /*
        burada gelen diyalog paketinde Answer var mı kontrol et
        varsa yanıt sayısına göre buton oluştur
        bu butonlar son cümlede ortaya çıksınlar
        Answer varsa -> Start diyalog diyerek yeniden diyalog başlat Answer içindeki diyaloğu yükle
        Answer yoksa -> end diyalog fonk çağır

        diyalog yükleme içinse oluşturulan butonlardan 
        */

        if (sentences.Count == 0)
        {
            if (mDialogueAnswers.Length != 0)
            {
                InitializeAnswers();
            }
            else
            {
                EndDialogue();
            }

            return;
        }

        string currentSentence = sentences.Dequeue();
        Debug.Log(currentSentence);

        dialogueText.SetText(currentSentence);
    }

    void InitializeAnswers()
    {
        Debug.Log("intialize çalıştı!");
        int answerSize = mDialogueAnswers.Length;
        CreateAnswerButtons(answerSize);

    }

    void CreateAnswerButtons(int answerSize)
    {
        switch (answerSize)
        {
            case 1:
                answerFirst.gameObject.SetActive(true);
                SetTextToAnswerButtons(1);
                break;
            case 2:
                answerFirst.gameObject.SetActive(true);
                answerSecond.gameObject.SetActive(true);
                SetTextToAnswerButtons(2);
                break;
            case 3:
                answerFirst.gameObject.SetActive(true);
                answerSecond.gameObject.SetActive(true);
                answerThird.gameObject.SetActive(true);
                SetTextToAnswerButtons(3);
                break;
        }
    }

    void SetTextToAnswerButtons(int size)
    {
        switch (size)
        {
            case 1:
                answerFirst.GetComponentInChildren<Text>().text = mDialogueAnswers[0].answer;
                break;
            case 2:
                answerFirst.GetComponentInChildren<Text>().text = mDialogueAnswers[0].answer;
                answerSecond.GetComponentInChildren<Text>().text = mDialogueAnswers[1].answer;
                break;
            case 3:
                answerFirst.GetComponentInChildren<Text>().text = mDialogueAnswers[0].answer;
                answerSecond.GetComponentInChildren<Text>().text = mDialogueAnswers[1].answer;
                answerThird.GetComponentInChildren<Text>().text = mDialogueAnswers[2].answer;
                break;
        }
    }

    public void ButtonClickListener(int buttonNumber)
    {
        Debug.Log("tıklanan buton : " + buttonNumber);

        // tıklanan butonun indexine göre yeni diyaloğu yükle

        switch (buttonNumber)
        {
            case 1:
                LoadNewDialogue(mDialogueAnswers[0].followingDialogue);
                break;
            case 2:
                LoadNewDialogue(mDialogueAnswers[1].followingDialogue);
                break;
            case 3:
                LoadNewDialogue(mDialogueAnswers[2].followingDialogue);
                break;
        }
    }

    void LoadNewDialogue(Dialogue newDialogue)
    {
        StartDialogue(newDialogue);
    }

    void EndDialogue()
    {
        Debug.Log("Diyalog sonlandı!");
        dialogueCanvas.SetActive(false);
    }
}

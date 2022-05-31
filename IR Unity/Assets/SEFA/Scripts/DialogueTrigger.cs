using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialog;
    public GameObject popUpDialog;

    private bool isPlayerInArea = false;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            //diyalog başlasın
            Debug.Log("Player alana girdi");
            isPlayerInArea = true;
            popUpDialog.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInArea)
        {
            Debug.Log(" E basıldı");
            popUpDialog.SetActive(false);
            TriggerDialogueManager();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerInArea = false;
        popUpDialog.SetActive(false);
    }

    void TriggerDialogueManager()
    {
        // Dialogue manager'dan StartDialogue() çağır
        FindObjectOfType<DialogueManager>().StartDialogue(dialog);
    }
}

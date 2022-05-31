using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Answer", menuName = "Answer")]
public class Answer : ScriptableObject
{
    // yanıt ne olacak
    // yanıt seçilirse açılacak yeni diyalog

    [TextArea(2, 5)]
    public string answer;

    public Dialogue followingDialogue;
}

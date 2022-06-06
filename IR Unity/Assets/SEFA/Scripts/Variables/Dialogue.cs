using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public string npcName;

    [TextArea(2, 5)]
    public string[] sentences;

    public Answer[] answers;

    // buraya ayrıca yanıt olacaksa eğer bu yanıtların ekleneceği alanlar da olacak
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueTemplate : ScriptableObject
{
    public string npcName;

    [TextArea(2, 5)]
    public string[] sentences;
}

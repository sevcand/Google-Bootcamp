using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Public Dialogue Set", fileName = "new Public Dialogue Set")]
public class PublicDialogueSet : ScriptableObject
{
    // cümlelerin yer aldığı bir liste barındıracak

    [TextArea(1, 5)]
    public string[] sentences;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public string speakerName;
    [TextArea(3, 10)]
    public string dialogueText;
 

    public Sprite speakerIcon;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    [SerializeField]
    private DialogueText[] _phrase;

    [SerializeField]
    private string _npcName;
    public DialogueText[] GetPhrase()
    {
        return _phrase;
    }

    public string GetNameNpc()
    {
        return _npcName;
    }

}

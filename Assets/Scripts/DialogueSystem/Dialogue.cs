using System.Collections;
using System;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    [SerializeField]
    private DialogueText[] _phrase;

    [SerializeField]
    private string _npcName;

    [SerializeField]
    public DialogueText[] GetPhrase()
    {
        return _phrase;
    }

    public string GetNameNpc()
    {
        return _npcName;
    }

}

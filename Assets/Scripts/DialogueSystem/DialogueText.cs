using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueText 
{
    [SerializeField]
    private string _phrase;
    [SerializeField]
    private string _btnSkip;
   
    public string GetPhrase()
    {
        return _phrase;
    }

    public string GetBtnSkip()
    {
        return _btnSkip;
    }
}

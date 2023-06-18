using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameNPC;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TextMeshProUGUI _btnSkip;

    [SerializeField]
    private GameObject _dialogueBox;

    private int _count = 0;
    private Dialogue _actualDialogue;
    public void Starter(Dialogue dialogue)
    {
        _count = 0;
        _actualDialogue = dialogue;

        NextPhrase();
    }

    public void NextPhrase()
    {
        if (_actualDialogue == null)
            return;
        if(_count == _actualDialogue.GetPhrase().Length)
        {
            _dialogueBox.gameObject.SetActive(false);
            _actualDialogue = null;
            _count = 0;
            return;
        }
        _nameNPC.text = _actualDialogue.GetNameNpc();
        _text.text = _actualDialogue.GetPhrase()[_count].GetPhrase();
        _btnSkip.text = _actualDialogue.GetPhrase()[_count].GetBtnSkip();
        _dialogueBox.gameObject.SetActive(true);
        _count++;
    }
}

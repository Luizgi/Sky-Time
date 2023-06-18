using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private CharMove CharMove;

    public float letterDelay = 0.05f;
    private Coroutine typingCoroutine;

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
        if(_count >= _actualDialogue.GetPhrase().Length)
        {
            EndDialogue();
            return;
        }
        if (typingCoroutine != null)
            return;

        _nameNPC.text = _actualDialogue.GetNameNpc();
        
        _text.text = _actualDialogue.GetPhrase()[_count].GetPhrase();
        _btnSkip.text = _actualDialogue.GetPhrase()[_count].GetBtnSkip();

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(_text.text));

        _dialogueBox.gameObject.SetActive(true);
        _count++;
    }
    private void EndDialogue()
    {
        _dialogueBox.gameObject.SetActive(false);
        _actualDialogue = null;
        _count = 0;
       
        
    }
    private IEnumerator TypeText(string text)
    {
        _text.text = "";
        foreach (char letter in text)
        {
            _text.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    public void ExitDialogue()
    {
        _exitButton.gameObject.SetActive(false); 
        EndDialogue();
    }
}

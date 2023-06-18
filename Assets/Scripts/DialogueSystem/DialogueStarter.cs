using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField]
    private Dialogue _dialogue;
    [SerializeField]
    private DialogueManager _manager;

    public void Starter(DialogueManager _manager)
    {
       if(_manager == null)
            return;
        _manager.Starter(_dialogue);
        
    }
}

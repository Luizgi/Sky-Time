using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public GameObject TutoE;
    private bool isTutoE = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowButton()
    {
        isTutoE = !TutoE;

        if (isTutoE)
        {
            TutoE.SetActive(true);
        }
        else
        {
            TutoE.SetActive(false);
        }
    }

    
}

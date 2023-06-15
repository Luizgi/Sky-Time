using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private bool isTutoE = false;
    public GameObject TutoE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) &&  !isTutoE)
        {

        }
    }
    public void ShowButton()
    {
        isTutoE = !isTutoE;

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

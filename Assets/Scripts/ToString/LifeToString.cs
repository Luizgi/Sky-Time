using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeToString : MonoBehaviour
{
    [SerializeField]private CharMove CM;
    [SerializeField]private Text MyText;

    void Update()
    {
        MyText.text = CM.GetHealth().ToString();   
    }
}

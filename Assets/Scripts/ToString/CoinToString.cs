using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinToString : MonoBehaviour
{
    [SerializeField] CharMove CM;
    [SerializeField] Text MyText;

    // Update is called once per frame
    void Update()
    {
        MyText.text = CM.GetCoin().ToString();
    }
}

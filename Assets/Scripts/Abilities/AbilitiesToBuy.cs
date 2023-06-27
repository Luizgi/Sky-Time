using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesToBuy : MonoBehaviour
{
    [SerializeField] private CharMove character;
    [SerializeField] private GameObject YDNHM;
    [SerializeField] public GameObject YAB;   
    //AbilityList
    [SerializeField] private bool Berserk = false;
    [SerializeField] private bool DeathKnight = false;
    [SerializeField] private bool Hunter = false;
    [SerializeField] private bool Ranger = false;
    [SerializeField] private bool Space = false;
    [SerializeField] private bool Druid = false;
    [SerializeField] private bool Rage = false;

    public void aBerserk()
    {
        if(character.coin >= 5 && Berserk == false)
        {
            character.SwordDamage = 15;
            character.coin = character.coin - 5;
            Berserk = true;
        }
        else if(Berserk == true)
        {
            StartCoroutine(AlreadyBuy());
        }
        else if(character.coin < 5)
        {
            StartCoroutine(DoNotHaveMoney());
        }
    }

    public void aDeathKnight()
    {
        Debug.Log("Apertou");
        if (character.coin >= 10)
        {
            character.SwordDamage = 25;
            character.coin = character.coin - 10;
            DeathKnight = true;
        }
        else if (DeathKnight == true)
        {
            StartCoroutine(AlreadyBuy());
        }
        else if (character.coin < 10)
        {
            StartCoroutine(DoNotHaveMoney());
        }

    }

    public void aHunter()
    {
        if(character.coin >= 5)
        {
            character.ArrowDamage = 10;
            character.coin = character.coin - 5;
            Hunter = true;
        }
        else if (Hunter == true)
        {
            StartCoroutine(AlreadyBuy());
        }
        else if (character.coin < 5)
        {
            StartCoroutine(DoNotHaveMoney());
        }
    }

    public void aRanger()
    {
        if (character.coin >= 10)
        {   
            character.ArrowDamage = 15;
            character.coin = character.coin - 10;
            Ranger = true;
        }
        else if (Ranger == true)
        {
            StartCoroutine(AlreadyBuy());
        }
        else if (character.coin < 10)
        {
            StartCoroutine(DoNotHaveMoney());
        }
    }

    public void aSpace()
    {
        if (character.coin >= 10)
        {
            character.ArrowMax = 20;
            character.coin = character.coin - 10;
            Space = true;
        }
        else if (Space == true)
        {
            StartCoroutine(AlreadyBuy());
        }
        else if (character.coin < 10)
        {
            StartCoroutine(DoNotHaveMoney());
        }
    }

    public void aDruid()
    {
        if (character.coin >= 10)
        {
            character.life = 150;
            character.coin = character.coin - 10;
            Druid = true;
        }
        else if (Druid == true)
        {
            StartCoroutine(AlreadyBuy());
        }
        else if (character.coin < 10)
        {
            StartCoroutine(DoNotHaveMoney());
        }
    }

    public void aRage()
    {
        if(character.coin >= 15)
        {
            character.SwordDamage = 30;
            character.ArrowDamage = 20;
            character.coin = character.coin - 15;
            Rage = true;
        }
        else if (Rage == true)
        {
            StartCoroutine(AlreadyBuy());
        }
        else if (character.coin < 15)
        {
            StartCoroutine(DoNotHaveMoney());
        }
    }

    IEnumerator DoNotHaveMoney()
    {
        YDNHM.SetActive(true);
        yield return new WaitForSeconds (5);
        DeactivateThis();
    }

    IEnumerator AlreadyBuy()
    {
        YAB.SetActive(true);
        yield return new WaitForSeconds(5);
        DeactivateThat();
    }

    private void DeactivateThis()
    {
        YDNHM.SetActive(false);
    }

    private void DeactivateThat()
    {
        YAB.SetActive(false);
    }
}

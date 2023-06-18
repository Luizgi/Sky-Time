using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour
{
    public int ActualPhase;

    // Start is called before the first frame update
    private void Start()
    {
        if(ActualPhase > 0 && ActualPhase < 6)
        {
            PlayerPrefs.SetInt("FASE", ActualPhase);
        }
    }

    // Update is called once per frame
    public void Fase(int numberPhase)
    {
        SceneManager.LoadScene(numberPhase);
    }

    public void PassPhase()
    {
        int pPhase = ActualPhase + 1;
        SceneManager.LoadScene(pPhase);
    }

    public void GoToSavedPhase()
    {
        int pSave = PlayerPrefs.GetInt("Fase");
        SceneManager.LoadScene(pSave);
    }

    public void Settings()
    {

    }

    public void Credits()
    {
        SceneManager.LoadScene("creditsScene");
    }
}

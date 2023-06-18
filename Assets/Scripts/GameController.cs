using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private bool isPaused = false;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject resumeButton;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else if (Settings.activeSelf) // Verifica se o menu de configura��es est� ativo
            {
                Settings.SetActive(false);
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResetGame()
    {
        // Implementar a l�gica para resetar o jogo
        // Exemplo:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void QuitGame()
    {
        // Implementar a l�gica para sair do jogo
        // Exemplo:
        Application.Quit();
    }

    public void GoToFirstScene()
    {
        // Implementar a l�gica para ir para a primeira cena
        // Exemplo:
        SceneManager.LoadScene(0);
        Resume();
    }

    public void GoToSettings()
    {
        Settings.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void ResumeGameFromSettings() 
    {
        Settings.SetActive(false);
        Resume();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    private bool isPaused = false;
 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;   

        if(isPaused) 
        {
            Time.timeScale = 0f;
            pauseMenuCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;    
            pauseMenuCanvas.SetActive(false);   
        }
    }
}

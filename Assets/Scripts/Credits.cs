using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Credits : MonoBehaviour
{
    public Text textComponent;
    public float scrollSpeed = 30f;
    public string firstSceneName = "StartScene";

    private RectTransform textTransform;
    private float scrollPosition;

    // Start is called before the first frame update
    void Start()
    {
        textTransform = textComponent.GetComponent<RectTransform>();
        scrollPosition = textTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        ScrollText();

        if(scrollPosition >= textTransform.rect.height)
        {
            ReturnToFirstScene();
        }
    }

    private void ScrollText()
    {
        scrollPosition += scrollSpeed * Time.deltaTime;
        textTransform.anchoredPosition = new Vector2(textTransform.anchoredPosition.x, scrollPosition); 

        if(scrollPosition >= textTransform.rect.height)
        {
            scrollPosition = -textTransform.rect.height;
        }
    }

    private void ReturnToFirstScene()
    {
        SceneManager.LoadScene("StartMenu");
    }
}

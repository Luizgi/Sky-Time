using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;

    [Header("Setting")]
    public float typingSpeed;
    private string[] sentences;
    [SerializeField] private int index;
    private bool isTyping = false;
    private bool isTextComplete = false;

    private Coroutine typingCoroutine;

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
        }

        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;

        typingCoroutine = StartCoroutine(TypeSentences());
    }

    IEnumerator TypeSentences()
    {
        isTyping = true;
        isTextComplete = false;
        speechText.text = "";
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        isTextComplete = true;
    }

    public void NextSentences()
    {
        if (isTyping)
        {
            // Se ainda estiver digitando, pular para o fim do texto
            StopCoroutine(typingCoroutine);
            speechText.text = sentences[index];
            isTyping = false;
            isTextComplete = true;
        }
        else
        {
            if (isTextComplete)
            {
                if (index < sentences.Length - 1)
                {
                    index++;
                    typingCoroutine = StartCoroutine(TypeSentences());
                }
                else
                {
                    speechText.text = "";
                    index = 0;
                    dialogueObj.SetActive(false);
                }
            }
        }
    }
}

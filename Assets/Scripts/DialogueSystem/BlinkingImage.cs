using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkingImage : MonoBehaviour
{
    public Image image;
    public float blinkInterval = 0.5f;

    private void Start()
    {
        StartCoroutine(BlinkImage());
    }

    private IEnumerator BlinkImage()
    {
        while (true)
        {
            image.enabled = !image.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
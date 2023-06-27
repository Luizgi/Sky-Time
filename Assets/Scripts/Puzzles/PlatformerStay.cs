using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerStay : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            //isCharacterOnPlatform = true;
            //characterTransform = collision.transform;
            Debug.Log("tocou");
            transform.parent = collision.gameObject.transform;
        }

    }
}

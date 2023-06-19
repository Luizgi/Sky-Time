using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechTxt;
    public string actorName;

    [SerializeField] private int radius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] public DialogueController controller;
    public float proximityDistance = 5f;
    bool onRadius;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<DialogueController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onRadius) 
        {
            controller.Speech(profile, speechTxt, actorName);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Interact();
        Interact3D(proximityDistance);
    }


    //Metodo Para jogo 2D
    /*public void Interact2D()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (hit != null)
        {
            onRadius = true;
            Debug.Log("Entrou Na Area");
        }
        else
        {
            onRadius = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    */
    public void Interact3D(float distance)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Character");

        if (player != null)
        {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);

            if (playerDistance <= distance)
            {
                onRadius = true;
                Debug.Log("Player está dentro da distância permitida");
            }
            else
            {
                onRadius = false;
                Debug.Log("Player está fora da distância permitida");
            }
        }
        else
        {
            onRadius = false;
            Debug.Log("Player não encontrado");
        }
    }

}

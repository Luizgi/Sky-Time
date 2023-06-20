using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slippery : MonoBehaviour
{
   public string slipperyTag = "Slippery";

    private Rigidbody rb;
    private Vector3 movementDirection;
    private bool isSlipperyActivated = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!isSlipperyActivated)
        {
            return;
        }

        // Obter entrada de movimento horizontal e vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcular a direção do movimento
        movementDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // Aplicar força ao Rigidbody para mover o personagem
        rb.AddForce(movementDirection * 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isSlipperyActivated)
        {
            // Verificar se colidiu com um objeto Slippery
            if (collision.gameObject.CompareTag(slipperyTag))
            {
                isSlipperyActivated = true;
            }
        }
        else
        {
            // Verificar se colidiu com uma pedra
            if (collision.gameObject.CompareTag("Stone"))
            {
                // Parar o movimento
                rb.velocity = Vector3.zero;
            }
        }
    }
}

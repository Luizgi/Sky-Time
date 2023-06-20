using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableRock : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxDistance = 10f;
    public Animator characterAnimator;

    private Rigidbody rb;
    private Vector3 initialPosition;
    private Transform playerTransform;
    private bool isMoving = false;
    private GameObject currentRock; // Refer�ncia para a pedra atualmente sendo empurrada

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;

        GameObject player = GameObject.FindGameObjectWithTag("Character");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            if (currentRock != null)
            {
                // Calcula a dire��o para onde a pedra deve se mover
                Vector3 moveDirection = playerTransform.forward;
                moveDirection.y = 0f;
                moveDirection.Normalize();

                // Move a pedra usando o Rigidbody
                rb.velocity = moveDirection * moveSpeed;

                // Verifica a dist�ncia em rela��o ao jogador
                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

                // Verifica se a pedra atingiu a dist�ncia m�xima
                if (distanceToPlayer >= maxDistance)
                {
                    StopMovingRock();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            rb.isKinematic = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            rb.isKinematic = true;
        }
    }

    private void StopMovingRock()
    {
        if (currentRock != null)
        {
            rb.velocity = Vector3.zero; // Para a pedra quando atinge a dist�ncia m�xima
            currentRock = null;
            rb = null;

            isMoving = false;

            // Reinicia a anima��o do personagem
            characterAnimator.SetBool("IsPushing", false);
        }
    }
}

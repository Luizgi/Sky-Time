using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableRock : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxDistance = 10f;
    public Animator characterAnimator;
    public LayerMask obstacleLayer; // Camadas consideradas obstáculos

    private Rigidbody rb;
    private Vector3 initialPosition;
    private Transform playerTransform;
    private bool isMoving = false;
    private GameObject currentRock; // Referência para a pedra atualmente sendo empurrada

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
                // Calcula a direção para onde a pedra deve se mover
                Vector3 moveDirection = playerTransform.forward;
                moveDirection.y = 0f;
                moveDirection.Normalize();

                // Verifica se há uma pedra na frente bloqueando o caminho
                RaycastHit hit;
                if (!Physics.Raycast(transform.position, moveDirection, out hit, maxDistance, obstacleLayer))
                {
                    // Move a pedra usando o Rigidbody
                    rb.velocity = moveDirection * moveSpeed;
                }
                else
                {
                    StopMovingRock();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            rb.isKinematic = false;
            characterAnimator.SetBool("Pushing", true);
            currentRock = gameObject; // Define a pedra atualmente sendo empurrada
            isMoving = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character") && currentRock == gameObject)
        {
            StopMovingRock();
        }
    }

    private void StopMovingRock()
    {
        rb.velocity = Vector3.zero; // Para a pedra quando atinge a distância máxima
        rb.isKinematic = true;
        characterAnimator.SetBool("Pushing", false);

        currentRock = null;
        isMoving = false;
    }
}

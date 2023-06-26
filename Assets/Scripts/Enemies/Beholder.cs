using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder : MonoBehaviour
{
    [SerializeField] private CharMove cm;
    private Transform target;
    public float moveSpeed = 3f;
    private Rigidbody rb;
    private Animator animator;
    private Vector3 initialPosition;
    private bool isChasing = false;

    public string enemyName;
    public int attackDamage;
    public int health;
    public int speed;
    public float attackRange;
    public float dodgeChance;
    public float followRadius = 10f; // Raio de distância para começar a seguir o personagem
    public float hitForce = 100f; // Força de empurrão quando tomar dano

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    private void Awake()
    {
        health = 50;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
            return;
        }

        if (isChasing && target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= followRadius)
            {
                // Movendo-se em direção ao personagem
                Vector3 direction = (target.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;

                // Atacar quando o personagem estiver dentro do alcance de ataque
                if (distance <= attackRange)
                {
                    animator.SetTrigger("attack");
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero; // Parar de se mover quando não estiver perseguindo
        }

        // Chamar animações apropriadas
        if (isChasing && target != null)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            isChasing = true;
            target = other.transform;
            CharMove charMove = other.GetComponent<CharMove>();

            if (charMove != null)
            {
                charMove.actualLife -= attackDamage;
            }
        }
        else if (other.CompareTag("CharSword"))
        {
            health -= cm.SwordDamage;
            animator.SetTrigger("getHit");

            // Aplicar força de empurrão para trás
            Vector3 hitDirection = transform.position - other.transform.position;
            hitDirection.y = 0f;
            hitDirection.Normalize();
            rb.AddForce(hitDirection * hitForce, ForceMode.Impulse);
        }
    }

    private void Die()
    {
        if (health <= 0)
        {
            Debug.Log("Morreu");
            animator.SetTrigger("die");
            Destroy(gameObject, 1f); // Destruir o objeto após 1 segundo (tempo para a animação de morte ser reproduzida)
        }
    }
}

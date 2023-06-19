using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

public class Beholder : MonoBehaviour
{
    public EnemyDatabase enemyDatabase;
    public Transform target;
    public float moveSpeed = 3f;
    private Rigidbody rb;
    private Vector3 initialPosition;
    private bool isChasing = false;
    private EnemyData enemyData;
    private bool isDodging;
    private Vector2 dodgeDirection;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;

        enemyData = enemyDatabase.GetEnemyData("Beholder");
        if (enemyData != null)
        {
             Debug.Log("Nome do inimigo: " + enemyData.enemyName);
               Debug.Log("Dano de ataque do inimigo: " + enemyData.attackDamage);
               Debug.Log("Vida do inimigo: " + enemyData.health);
               Debug.Log("Vida do inimigo: " + enemyData.speed);
               Debug.Log("Vida do inimigo: " + enemyData.attackRange); 
               Debug.Log("Vida do inimigo: " + enemyData.dodgeChance);
            Debug.Log("Encontrou");


            enemyData.health = enemyData.maxHealth;
        }
        else
        {
            Debug.Log("Inimigo não encontrado!");
        }
    }

    private void Update()
    {
        if (target != null && isChasing)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            Vector3 direction = (initialPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, enemyData.player.position);

        if(distanceToPlayer <= enemyData.attackRange && !isDodging)
        {
            TryDodge();
        }

        if (isDodging)
        {
            PerformDodge();
        }
        else
        {
            PerformNormalAttack();
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
                charMove.actualLife -= enemyData.attackDamage;
            }
        }
    }
    private void TryDodge()
    {
        if (Random.value <= enemyData.dodgeChance)
        {
            isDodging = true;
            dodgeDirection = Random.insideUnitCircle.normalized;
        }
    }

    private void PerformDodge()
    {
        rb.velocity = new Vector3(dodgeDirection.x * enemyData.speed, rb.velocity.y, dodgeDirection.y * enemyData.speed);

        isDodging = false;
    }

    private void PerformNormalAttack()
    {
        transform.LookAt(enemyData.player);
        transform.Translate(Vector3.forward * enemyData.speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

public class Beholder : MonoBehaviour
{
    [SerializeField] CharMove cm;
    public EnemyDatabase enemyDatabase;
    public Transform target;
    public float moveSpeed = 3f;
    private Rigidbody rb;
    private Vector3 initialPosition;
    private bool isChasing = false;
    private EnemyData enemyData;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;

        enemyData = enemyDatabase.GetEnemyData("Beholder");
        if (enemyData != null)
        {
            enemyData.health = enemyData.maxHealth;
        }
    }

    private void Update()
    {
        if (enemyData != null)
        {
            if (enemyData.health <= 0)
            {
                Die(enemyData.maxHealth);
            }
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
        if (other.CompareTag("CharSword"))
        {
            enemyData.health -= cm.SwordDamage;
            Debug.Log("Perdeu: " + cm.SwordDamage);
        }
    }

    private void Die(float maxHealth)
    {
        if (maxHealth < 0)
        {
            Debug.Log("Morreu");
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemies
{
    [System.Serializable]
    public class EnemyData
    {
        public string enemyName;
        public int attackDamage;
        public int health;
        public int maxHealth;
        public int speed;
        public float attackRange;
        public float dodgeChance;
        public Transform player;
    }
}
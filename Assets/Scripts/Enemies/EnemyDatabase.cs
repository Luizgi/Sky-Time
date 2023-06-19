using UnityEngine;
using Enemies;

public class EnemyDatabase : MonoBehaviour
{
    public Enemies.EnemyData[] enemyDataArray;
    public Enemies.EnemyData GetEnemyData(string enemyName)
    {
        foreach (Enemies.EnemyData enemyData in enemyDataArray)
        {
            if (enemyData.enemyName == enemyName)
            {
                return enemyData;
            }
        }

        return null;
    }
}
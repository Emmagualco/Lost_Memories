using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabEnemy;
    public int maxEnemy = 5;
    public int enemySpawned = 1;
    private float timer;
    void Start()
    {
        timer = Time.time + 3;
        InvokeRepeating("GenerateEnemy", 0, 3);
    }


    void Update()
    {

    }

    void GenerateEnemy()
    {
        if (enemySpawned < maxEnemy)
        {
            if (timer < Time.time)
            {
                Instantiate(prefabEnemy, transform.position, transform.rotation);
                enemySpawned++;
                timer = Time.time + 3;
            }
        }
    }
    //void EndGame()
    //{
    //    if (true)
    //    {

    //    }
    //}
}

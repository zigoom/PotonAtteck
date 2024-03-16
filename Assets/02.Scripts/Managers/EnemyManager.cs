using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float delayTime = 7f;
    public float swawnTime = 3f;
    public GameObject enemy;
    public Transform[] spawanPoints;
    public PlayerHealth playerHealth;

    public int maxCnt = 5;
    int newCnt = 0;
       
    void Start()
    {
        //print("starting!");
        // 1초 후에 해당 메소드 실행되도록
        //Invoke("Spawn", 1f);

        // 메소드 이름, 시작시간, 반복시간
        // 해당 메소드를 1초후에 2초마다 실행
        InvokeRepeating("Spawn", delayTime, swawnTime);
        
    }
    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f || newCnt>=maxCnt)
        {
            return;
        }

        //print("Enemy Screate");
        int spawnPoint = Random.Range(0, spawanPoints.Length);
        Instantiate(enemy, spawanPoints[spawnPoint] .position,spawanPoints[spawnPoint].rotation);

        newCnt++;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

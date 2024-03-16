using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;               
    PlayerHealth playerHealth;      
    EnemyHealth enemyHealth;            
    protected UnityEngine.AI.NavMeshAgent nav;                  

    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {

            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}

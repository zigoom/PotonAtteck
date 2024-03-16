using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{

    public int startingealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public Slider enemyHP;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    SDManager gameManager;


    //GameObject textOBJ;
    //ScoreManager scoreManager;

    private void Awake()
    {
        //textOBJ = GameObject.Find("Score Text");
        //scoreManager = textOBJ.GetComponent<ScoreManager>();

        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        gameManager = GameObject.Find("GameManager").GetComponent< SDManager>();

        currentHealth = startingealth;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);  // 로컬좌표
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;
        enemyAudio.Play();
        currentHealth -= amount;
        enemyHP.value = currentHealth;
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {

        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        gameManager.EnmeyDie();
    }

    //  에니메이션 이벤트에서 호출하는 이벤트
    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        ScoreManager.static_score += scoreValue;
        Destroy(gameObject, 2f);
    }

}

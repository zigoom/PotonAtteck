using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;


public class PlayerHealth : MonoBehaviourPun, IPunObservable
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider sliderHP;  // HP 슬라이더 
    public Text textHP;      // HP 텍스트

    Animator anim;
    //AudioSource playerAudio;
    PlayerMove playerMove;
    PlayerShooting plyerShooting;
    bool isDead;
    // bool damaged;
    string strHp;

    
    public Text myHP_txt;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        plyerShooting = GetComponentInChildren<PlayerShooting>();
        
        if (photonView.IsMine){
            myHP_txt = GameObject.Find("MyHP").GetComponent<Text>();
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        if (photonView.IsMine){
            myHP_txt.text = "HP: "+currentHealth.ToString();
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        
        // if (photonView.IsMine){
        //     myHP_txt.text = "HP: "+currentHealth.ToString();
        // }

    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
            
        // damaged = true;        
        if (currentHealth <= 0) currentHealth = 0; // 체력이 0보다 작으면 0으로
        // // 데미지를 받은 후에 체력을 갱신하고 동기화
        photonView.RPC("SyncHealth", RpcTarget.All, currentHealth);
    }

    [PunRPC]
    void SyncHealth(int health)
    {        
        if(photonView.IsMine){
            Debug.Log("나 맞았음");

            currentHealth = health;
            myHP_txt.text = "HP: "+currentHealth.ToString();

            if (currentHealth <= 0 )
            {
                StartCoroutine(EndGameAfterDelay(5f));
            } 
        }else{            
            Debug.Log("너 맞았음");
            
            currentHealth = health;
            sliderHP.value = currentHealth; // 체력바
            textHP.text = "HP : " + currentHealth.ToString(); // 체력 텍스트
            
            if (currentHealth <= 0)
            {
                Death();
            } 
        }
        // currentHealth = health;
        
        // sliderHP.value = currentHealth;                     // 체력바
        // textHP.text = "HP : " + currentHealth.ToString();   // 체력 텍스트
        // if (photonView.IsMine){
        //     myHP_txt.text = "HP: "+currentHealth.ToString();
        // }
        
        // Debug.Log("현재 체력: " + currentHealth);
    }


    void Death()
    {
        isDead = true;
        plyerShooting.DisableEffects ();
        anim.SetTrigger("Die");
        playerMove.enabled = false;
        plyerShooting.enabled = false;
    }


    public void RestartLevel()
    {
        
        SceneManager.LoadScene(0);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // if (stream.IsWriting)
        // {
        //     currentHealth--;
        //     stream.SendNext(currentHealth);
        // }
        // else
        // {
        //     currentHealth = (int)stream.ReceiveNext();
        //     sliderHP.value = currentHealth;                     // 체력바
        //     textHP.text = "HP : " + currentHealth.ToString();   // 체력 텍스트

        // }
    }

    IEnumerator EndGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (currentHealth <= 0)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }   
    }
}

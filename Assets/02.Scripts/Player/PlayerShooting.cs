using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviourPun ,IPunObservable
{
    public Transform gunTransform;
    public int damagePerShot = 25;
    public float timeBetweenBullets = 0.15f;
    public int attack = 10;
    int bulletCount;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    public int shootableMask;
    public LineRenderer gunLine;
    AudioSource gunAudio;
    public Light gunLight;
    float effectsDisplayTime = 0.15f;
    GameObject arm;
    Animator armAnimator;
    Text bullet;
    string printBullet;

    float maxLength = 100f;

    public GameObject gunBarrelEnd;

    private void Awake()
    {
        shootableMask = LayerMask.NameToLayer("Shootable");
        arm = GameObject.Find("arms_assault_rifle_01");
        gunTransform = GameObject.Find("assault_rifle_01_iron_sights").transform;
        gunBarrelEnd = GameObject.Find("GunBarrelEnd");
        gunLine = gunBarrelEnd.GetComponent<LineRenderer>();
        gunAudio = gunBarrelEnd.GetComponent<AudioSource>();
        gunLight = gunBarrelEnd.GetComponent<Light>();

        armAnimator = arm.GetComponent<Animator>();
        bullet = GameObject.Find("CurrentBullet").GetComponent<Text>();
        bulletCount = 30;

    }

    void Update()
    {
        if (!photonView.IsMine) return;

        timer += Time.deltaTime;
               
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        if (0 >= bulletCount){ // 총알이 0개 이거나 음수일 경우
            
            DisableEffects();

            armAnimator.SetBool("isReload", true);
            // timer = -3f;

            printBullet = "Bulllet: " + bulletCount;
            bullet.text = printBullet;
                
            // 현재 재생 중인 애니메이션 상태 정보 가져오기
            AnimatorStateInfo stateInfo = armAnimator.GetCurrentAnimatorStateInfo(0);
            // 현재 재생 중인 애니메이션의 이름을 가져옴
            string currentAnimationName = armAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            // Debug.Log("1_"+currentAnimationName);
            // Debug.Log("2_"+stateInfo.normalizedTime);
            // 현재 재생 중인 애니메이션이 "Reload" 애니메이션인지 확인
            if (currentAnimationName == "reload_ammo_left@assault_rifle_01" && stateInfo.normalizedTime >= 0.95f)
            {
                bulletCount = 30;
                armAnimator.SetBool("isReload", false);
            }
        } else {
            printBullet = "Bulllet: " + bulletCount;
            bullet.text = printBullet;

            // 발사 버튼이 눌리고, 누르는 시간차가 timeBetweenBullets 이상이며, 일시정지가 아니라 시간이 제대로 흐르고 있을떄에
            if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
            {            
                if (photonView.IsMine)
                {
                    --bulletCount;

                    photonView.RPC("Shoot", RpcTarget.All);
                    gunAudio.Play();
                    timer =0;
                }
            }
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
    
    [PunRPC]
    void Shoot()
    {
        if (photonView.IsMine && !armAnimator.GetBool("isReload"))
            armAnimator.CrossFadeInFixedTime("Fire", 0.01f);
        timer = 0f;      

        gunLine.enabled = true;
        gunLight.enabled = true;

        // 선의 시작점은 현재 객체의 위치로 설정
        Vector3 startPosition = gunLine.transform.position;
        gunLine.SetPosition(0, startPosition);

        // 선의 끝점을 충돌 지점으로 설정
        RaycastHit hit;
        if (Physics.Raycast(gunLine.transform.position, gunLine.transform.up, out hit, maxLength))
        {
            // 충돌 지점 설정
            gunLine.SetPosition(1, hit.point);

            if(hit.collider.gameObject.layer==shootableMask && gameObject != hit.collider.gameObject){
                PlayerHealth playerHealth = hit.collider.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attack);
                }
            }
        }
        else
        {   // 최대 길이에 도달하면 최대 길이만큼 그림
            gunLine.SetPosition(1, gunLine.transform.position + gunLine.transform.up * maxLength);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SDManager : MonoBehaviour
{
    public Text startTxt;
    public Text startTimeTxt;
    public Text enemyCnt;
    public Text gameEnd;

    public AudioClip startAudio;
    public AudioClip playAudio;
    public AudioClip endAudioBad;
    public AudioClip endAudioGood;
    public AudioSource audioSource;

    bool gameState = true;

    int enemyTotal = 15;
    public int enemyNew = 0;

    float startTimer = 0;
    int startTime = 10;

    GameObject player;
    PlayerHealth playerHealth;

    bool isDead=false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyCnt.text = "남은 적의 수 : 15 / 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState)
        {
            startTimer += Time.deltaTime;
            if (startTimer >= 1f)
            {
                startTime--;
                startTimeTxt.text = startTime.ToString();
                startTimer = 0;
            }

            if (startTime <= 0)
            {
                gameState = false;
                startTxt.enabled = false;
                startTimeTxt.enabled = false;
                audioSource.clip = playAudio;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        if (playerHealth.currentHealth <= 0 && !isDead)
        {
            audioSource.loop = false;
            audioSource.clip = endAudioBad;

            string m_text = "ㅠ 게임 패배 ㅠ";
            string m_text2 = "Lost";

            startTxt.text = "";
            startTxt.enabled = true;

            gameEnd.text = "";
            gameEnd.enabled = true;

            isDead = true;

            StartCoroutine(_typing(m_text, m_text2));
        }
    }

    public void EnmeyDie()
    {
        enemyNew++;
        enemyCnt.text = "남은 적의 수 : 15 / " + enemyNew.ToString();
        if(enemyNew>= enemyTotal)
        {
            audioSource.loop = false;
            audioSource.clip = endAudioGood;

            string m_text = "!! 게임 승리 !!";
            string m_text2 = "Win";

            startTxt.text = "";
            startTxt.enabled = true;

            gameEnd.text = "";
            gameEnd.enabled = true;

            StartCoroutine(_typing(m_text, m_text2));
        }
    }

    IEnumerator _typing(string m_text, string m_text2)
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= m_text.Length; i++)
        {
            startTxt.text = m_text.Substring(0, i);

            yield return new WaitForSeconds(0.15f);
        }
        for (int i = 0; i <= m_text2.Length; i++)
        {
            gameEnd.text = m_text2.Substring(0, i);

            yield return new WaitForSeconds(0.15f);
        }
        audioSource.Play();
    }
}

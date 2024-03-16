using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int static_score;
    public int score;

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        static_score = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "score : " + static_score;
    }
}

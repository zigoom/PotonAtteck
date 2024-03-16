using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimation : MonoBehaviour
{
    Animator animator;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReloadEnd()
    {
        
        animator.SetBool("isReload", false);
    }


}

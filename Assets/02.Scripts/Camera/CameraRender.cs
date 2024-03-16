using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class CameraRender : MonoBehaviourPun
{

    GameObject arm;
    SkinnedMeshRenderer[] skinnedMesh;


    //Animator armAnimator;

    // Start is called before the first frame update
    void Start()
    {
        arm = GameObject.Find("arms_assault_rifle_01");
      
         
        // armAnimator = arm.GetComponent<Animator>();
         skinnedMesh = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!photonView.IsMine) return; //  포톤 뷰 컴포넌트가 내 컴포넌트가 맞는지

        if (photonView.IsMine)
        {
            arm.SetActive(true);
            skinnedMesh[0].enabled = false;
            skinnedMesh[1].enabled = false;
        }

        else
        {
            arm.SetActive(false);
            skinnedMesh[0].enabled = true;
            skinnedMesh[1].enabled = true;
        }

    }
}

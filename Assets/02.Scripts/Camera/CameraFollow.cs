using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviourPun
{
    Transform target;
    public float smoothing = 5f;
    public float y = 0f, z = 0f;
    Vector3 offset;
    float camRayLength = 100f;
    int floorMask;
    Rigidbody playerRigidbody;
    SkinnedMeshRenderer[] skinnedMesh;
    public Slider sliderHP;
    public Text textHP;

    private Transform camTr;
    // Start is called before the first frame update
    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();
        camTr = Camera.main.GetComponent<Transform>();
        //offset = target.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.IsMine) return; //  포톤 뷰 컴포넌트가 내 컴포넌트가 맞는지
            
        if (photonView.IsMine)
        {
            offset.Set(transform.position.x, transform.position.y+0.5f, transform.position.z -0.25f);

            Camera.main.transform.position = offset;
            Camera.main.transform.rotation = transform.rotation;
        }

        //transform.position = target.transform.position;
        //transform.rotation = target.rotation;

        //Vector3 targetCampos = transform.position + offset;
        //target.position = Vector3.Lerp(target.position, targetCampos, smoothing * Time.deltaTime);

        // Turnning();
    }

    private void LateUpdate()
    {
        sliderHP.transform.LookAt(camTr.position);
        textHP.transform.LookAt(camTr.position);
        textHP.transform.eulerAngles += new Vector3(0, 180f, 0);

    }

    void Turnning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

}

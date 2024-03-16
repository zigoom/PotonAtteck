using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayer : MonoBehaviour
{
    private Transform camTr;
    private Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        camTr = Camera.main.GetComponent<Transform>();
        tr = GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        tr.LookAt(camTr.position);
    }
}

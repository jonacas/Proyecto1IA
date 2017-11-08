using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton : MonoBehaviour {

    public Animator anim;

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e")) { anim.enabled = true; }
        
    }
}

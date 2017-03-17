using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour {

    public Animator anim;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A)) {
            anim.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.D)) {
            anim.SetInteger("Direction", 3);
        }
        else if (Input.GetKey(KeyCode.W)) {
            anim.SetInteger("Direction", 2);
        }
        else if (Input.GetKey(KeyCode.S)) {
            anim.SetInteger("Direction", 4);
        }
        else {
            anim.SetInteger("Direction", 0);
        }
    }
}

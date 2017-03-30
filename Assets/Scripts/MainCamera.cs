using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    GameObject player;
    GameObject background;

    public double bottom = -1.5;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        background = GameObject.Find("Background");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y, -2.7f);

        if ((double)pos.y < bottom) {
            pos.y = (float) bottom;        
        }

        transform.position = pos;
	}
}

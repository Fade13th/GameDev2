using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    GameObject player;
    GameObject background;

    public double bottom = -1.5;
    public float zoomClose, zoomFar, zoomCurrent;
    public float zoomSensitivty;


    // Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        background = GameObject.Find("Background");
	    zoomCurrent = Camera.main.fieldOfView;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    zoomCurrent -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivty;
	    zoomCurrent = Mathf.Clamp(zoomCurrent, zoomClose, zoomFar);
	    Camera.main.fieldOfView = zoomCurrent;
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y, -2.7f);
        
        if ((double)pos.y < bottom) {
            pos.y = (float) bottom;        
        }

        transform.position = pos;
	}
}

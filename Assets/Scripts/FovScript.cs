using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovScript : MonoBehaviour {

    public bool PlayerSeen { get; set; }

    private Color _playerVisible, _playerNotVisible;

	// Use this for initialization
	void Start () {
		_playerVisible = Color.red;
		_playerVisible.a = 60f/255f;
		_playerNotVisible = Color.green;
		_playerNotVisible.a = 60f/255f;
        GetComponent<SpriteRenderer>().color = _playerNotVisible;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PlayerSeen = true;
            GetComponent<SpriteRenderer>().color = _playerVisible;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player") { 
            PlayerSeen = false;
            GetComponent<SpriteRenderer>().color = _playerNotVisible;
        }
}

}

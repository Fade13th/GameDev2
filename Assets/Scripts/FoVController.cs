using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public class FoVController : MonoBehaviour {

    public bool PlayerSeen { get; set; }

    private Color _playerVisible, _playerNotVisible;

    // Use this for initialization
    void Start() {
        _playerVisible = Color.red;
        _playerNotVisible = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            if (PlayerSeen)
            {
                sprite.color = _playerVisible;
            }
            else
            {
                sprite.color = _playerNotVisible;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == "Player")
        {
            Vector3 origin = transform.parent.position;
            Vector3 direction = collider.transform.position - origin;
            RaycastHit2D raycast = Physics2D.Raycast(origin, direction);
            if (raycast.collider == collider)
            {
                PlayerSeen = true;
            }
            else
            {
                PlayerSeen = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "Player") {
            PlayerSeen = false;
        }
    }

}
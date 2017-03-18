using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;

    public Animator anim;

    //Grounded Vars
    bool isGrounded = true;

    void Update() {
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W)) {
            if (isGrounded) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
                isGrounded = false;
            }
        }

        moveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            moveVelocity = -speed;
            anim.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            moveVelocity = speed;
            anim.SetInteger("Direction", 3);
        }
        else
            anim.SetInteger("Direction", 0);

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);

    }
    //Check if Grounded
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Floor")
            isGrounded = true;
    }
}
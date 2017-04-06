using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;
    public bool JumpButtonPressed { get; set; }

    public int cutsceneWalk = 0;

    public Animator anim;

    public bool falling = false;

    //Grounded Vars
    public bool isGrounded = true;
    private static PlayerController _playerController;

    void Awake()
    {
        PlayerController._playerController = this;
    }

    void Start() {
        JumpButtonPressed = false;
    }

    void Update() {
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W)) {
            JumpButtonPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.W)) {
            JumpButtonPressed = false;
        }

        if (JumpButtonPressed && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            isGrounded = false;
            JumpButtonPressed = false;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platforms"), true);
        }


        if (Input.GetKey(KeyCode.S)) {
            falling = true;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platforms"), true);
        }
        else if(Input.GetKeyUp(KeyCode.S) || GetComponent<Rigidbody2D>().velocity.y < 0 ) {
            falling = false;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platforms"), false);
        }

        moveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || cutsceneWalk == -1) {
            moveVelocity = -speed;
            anim.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || cutsceneWalk == 1) {
            moveVelocity = speed;
            anim.SetInteger("Direction", 3);
        }
        else
            anim.SetInteger("Direction", 0);

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);

    }

    //Check if Grounded
    void OnTriggerEnter2D(Collider2D collider) {
        if ((collider.tag == "Floor" || collider.tag == "Platform") && collider.transform.position.y < transform.position.y) {
            falling = false;
            isGrounded = true;
        }
    }

    public static PlayerController GetPlayer()
    {
        return _playerController;
    }
}
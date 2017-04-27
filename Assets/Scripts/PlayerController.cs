using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public bool cutscene = false;

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;
    public bool JumpButtonPressed { get; set; }

    private int cutsceneWalk;

    public Animator anim;

    public float falling = 0;

    //Grounded Vars
    public bool isGrounded = true;
    private static PlayerController _playerController;

    void Awake() {
        PlayerController._playerController = this;
        gameObject.layer = LayerMask.NameToLayer("Player");
        cutsceneWalk = 0;
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

            if (JumpButtonPressed && isGrounded  && !cutscene) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
                isGrounded = false;
                JumpButtonPressed = false;
            }


            if (Input.GetKeyDown(KeyCode.S) && !cutscene) {
                falling = Time.time;
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platforms"), true);
            } else if (GetComponent<Rigidbody2D>().velocity.y > 0 )
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platforms"), true);
            }else if ((Input.GetKey(KeyCode.S) && !cutscene) || falling + 0.3 > Time.time)
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platforms"), true);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platforms"), false);
            }

        if (!cutscene) {
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
        }
        else {
            if (cutsceneWalk == -1) {
                moveVelocity = -speed;
                anim.SetInteger("Direction", 1);
            }
            else if (cutsceneWalk == 1) {
                moveVelocity = speed;
                anim.SetInteger("Direction", 3);
            }
            else {
                moveVelocity = 0;
                anim.SetInteger("Direction", 0);
            }
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
    }

    //Check if Grounded
    void OnTriggerEnter2D(Collider2D collider) {
        if ((collider.tag == "Floor" || collider.tag == "Platform") && collider.transform.position.y < transform.position.y) {
            isGrounded = true;
        }
    }

    public static PlayerController GetPlayer()
    {
        return _playerController;
    }

    public void setCutsceneDirection(int i) {
        cutsceneWalk = i;
    }
}
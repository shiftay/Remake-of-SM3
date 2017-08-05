using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    //----------------------------
    //         Properties
    //----------------------------
    #region Properties
    const float groundRadius = 0.1f;
    const float ceilingRadius = 0.01f;
    [SerializeField]
    public float maxSpeed = 1f;
    [SerializeField]
    private float jumpForce = 150f;
    private float airTime = 0f;
    [SerializeField]
    // private float maxAirTime = 0.7f;  Might have use later.
    private float fireBallVelocity;
    

    [SerializeField]
    private bool airControl = false;
    [SerializeField]
    private bool isGrounded;
    private bool isFacingRight = true;
    public bool isSprinting = false;


    public AudioSource m_MovementAudio;
    public AudioClip m_jumpClip;
    public AudioClip m_fireBall;


    [SerializeField]
    private LayerMask whatIsGround;      // LayerMask used to determine what is the 'ground' to the player
    private Transform groundCheck;       // position marking where to check for ground collisions
    private Transform ceilingCheck;      // position marking where to check for ceiling collisions
    private Animator animator;           // reference to animator component
    private Rigidbody2D rigidbody;     // reference to rigidbody component



    #endregion

    // Awake is called before Start() and even if the object is NOT Enabled
    void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
        

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        isGrounded = false;  // resets to false to run checks again

        // pulls in an array of collisions based ona circle cast around teh groundcheck
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, whatIsGround);

        for (int i = 0; i < colliders.Length; i++) // loops through the collider array
        {
            if (colliders[i].gameObject != gameObject) // if it hits something that isn't the player
            {
                isGrounded = true; // sets the grounded to true
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    // Moving around
    public void Move(float move, bool isJumping, float speed)
    {
        // checks for direction to check if he is running left or right.
        if (move * speed > 0)
            isFacingRight = true;
        else if (move * speed < 0)
            isFacingRight = false;
        // sets animations bools, as well as a holder for sprinting.
        if (speed > maxSpeed)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isSprinting", true);
            isSprinting = true;
        }

        // if the player is grounded or air control is on
        if (isGrounded || airControl)
        {
            // Move the character
            if (!isSprinting && move != 0)
            {
                animator.SetBool("isWalking", true);
            }
            rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);
            // flips sprite dependant on direction
            if (isFacingRight)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
                transform.localScale = new Vector3(-1f, 1f, 1f);

        }

        //  grounded and jump key is pressed
        if (isGrounded && isJumping)
        {

            animator.SetBool("isWalking", false);

            animator.SetBool("isJumping", true);
            animator.Play("Jump");

            // to allow flying


            airTime += Time.deltaTime;
            //checks our holder to alter the jump force based off of sprinting
            if (isSprinting)
            {
                jumpForce = 175f;
            }
            else
            {
                jumpForce = 150f;
            }

            rigidbody.AddForce(new Vector2(0f, jumpForce)); // jump
            // playing audio for jumping
            m_MovementAudio.clip = m_jumpClip;
            m_MovementAudio.Play();
            isGrounded = false; // set grounded to false for later loops

            

        }
        //reset the holder so we can use the next run through
        isSprinting = false;
    }





}

using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputControl : MonoBehaviour
{

    #region Properties
    private GameObject pauseText;
    private BoxCollider2D _playerTail;
    private GameManager checkState;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private Slider p_Slider;
    bool start = true;
    Vector2 offset = new Vector2(0.1f, 0f);



    private float jumpForce = 125f;
    float cooldown = 0.5f;
    float speed;
    float maxTime = 0.1f;
    float timer;
    bool canJump;
    bool grounded;
    bool canShoot = true;
    bool isFacingRight = true;
    [SerializeField]
    bool flying = false;
    [SerializeField]
    private float timerofSprint = 1f;
    public float maxSpeed = 1f;
    public float runMultiplier = 2f;
    private bool isSprinting = false;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    bool actualFlight = false;
    [SerializeField]
    bool flightCamera = false;
    public int jumpCount = 0;

    public AudioSource m_ShootingAudio;
    public AudioClip m_FireClip;
    public AudioClip m_TailSwipe;
    public AudioClip m_jumpClip;
    public AudioClip m_SprintClip;
    public GameObject m_FireBall;
    public Transform point1;
    public Transform point2;
    public LayerMask onlyGroundMask;
    public Vector2 velocity;
    public bool crouching = false;
   

    public bool FlightCamera
    {
        get { return flightCamera; }
        set { flightCamera = value; }
    }

    public bool Flying
    {
        get { return actualFlight; }
        set { actualFlight = value; }
    }

    #endregion


    // Use this for initialization
    void Awake()
    {
        p_Slider = GameObject.Find("Canvas/sprintSlider").GetComponent<Slider>();

        animator = GetComponent<Animator>();
        checkState = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pauseText = GameObject.Find("Canvas/pauseScreen");
        grounded = Physics2D.OverlapArea(point1.position, point2.position, onlyGroundMask);

        if (grounded)
        {
            actualFlight = false;
            rigidbody.gravityScale = 1;
           
        }

        // pausing the game
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 0)
            {
                checkState.pauseText.GetComponent<Text>().enabled = false;
                Time.timeScale = 1;
            }
            else
            {
                checkState.pauseText.GetComponent<Text>().enabled = true;
                Time.timeScale = 0;
            }
        }




        // basic jump
        if (grounded && Input.GetButtonDown("Jump"))
        {
            timer = 0;
            canJump = true;
            rigidbody.AddForce(new Vector2(0, jumpForce));
            m_ShootingAudio.clip = m_jumpClip;
            m_ShootingAudio.Play();
            animator.SetBool("isJumping", true);
        }

        // flying
        if(grounded && Input.GetButtonDown("Jump") && flying)
        {
            // actually fly
            if(checkState.PlayerState == 4)
            {
                
                marioFly();
   
            }
            // long jump
            else
            {
                rigidbody.AddForce(new Vector2(10f, 20f));
                rigidbody.gravityScale = 0.5f;
                flightCamera = true;
                StartCoroutine(CanShoot(2f));
                rigidbody.gravityScale = 0.75f;
            }
        }

        // while flying add more force
        if (Input.GetButtonDown("Jump") && actualFlight)
        {
            if (jumpCount < 5)
                rigidbody.velocity = new Vector2(1f, 2f);
            StartCoroutine(CanShoot(0.5f));
            //play animation for tail wag
        }
        
        // shootfireball
        if (Input.GetButtonDown("Fire3") && canShoot && checkState.PlayerState == 3)
        {
            Fire();
        }

        // tailswipe
        if (Input.GetButtonDown("Fire3") && canShoot && checkState.PlayerState == 4)
        {
            Swipe();
        }

        if(Input.GetButtonDown("Jump") && checkState.OnPipe)
        {
            checkState.Teleport = true;
        }

        // crouch
        if(Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                animator.SetBool("isCrouching", true);
                crouching = true;
                if (checkState.PlayerState > 1)
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size -= new Vector2(0f, 0.06f);
            }
            if (Input.GetAxis("Vertical") < 0 && checkState.OnPipe)
            {
                checkState.Teleport = true;
            }
        }

        //let go of crouch
        if (Input.GetButtonUp("Vertical") && Input.GetAxis("Vertical") < 0)
        {
            checkState.Teleport = false;
            animator.SetBool("isCrouching", false);
            crouching = false;
            if (checkState.PlayerState > 1)
                GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size += new Vector2(0f, 0.06f);
        }


        // let go of sprint
        if (Input.GetButtonUp("Fire3"))
        {
            timerofSprint = 1;
            p_Slider.value = 0;
            flying = false;
        }

    }

    void FixedUpdate()
    {
         
        // reseting animation bools so Move can use them properly each time.
        animator.SetBool("isSprinting", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("fullSpeed", false);

       // p_Slider.value = 0;

        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        
        // if statement to set speed, if shift is being held down.

        //while sprint is held
        if (Input.GetButton("Fire3"))
        {
            timerofSprint += Time.deltaTime;
            if (timerofSprint < runMultiplier)
                runMultiplier = timerofSprint;
            else
                runMultiplier = 2f;

            speed = maxSpeed * runMultiplier;
            p_Slider.value = timerofSprint;
            if (timerofSprint > 4)
            {
                animator.SetBool("fullSpeed", true);
                flying = true;
                if (!m_ShootingAudio.isPlaying)
                {
                    m_ShootingAudio.clip = m_SprintClip;
                    m_ShootingAudio.Play();
                }
            }
        }
        else
            speed = maxSpeed;

           // jump
    	if (grounded && Input.GetButtonDown("Jump")) {
						
		} else if (Input.GetButton("Jump") && canJump && timer<maxTime) {
				timer += Time.deltaTime;
				rigidbody.AddForce (new Vector2(0, jumpForce * 0.1f));
		} else {
				canJump = false;
		}


        // sets animations bools, as well as a holder for sprinting.
        if (speed > maxSpeed)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isSprinting", true);
            isSprinting = true;
        }

        // if the player is grounded or air control is on
        if (airControl || grounded && !crouching)
        {
            // Move the character
            
            
            if (!isSprinting && h != 0)
            {
                animator.SetBool("isWalking", true);
            }
            rigidbody.velocity = new Vector2(h * speed, rigidbody.velocity.y);
            // check which way we're going
            if (h * speed > 0)
                isFacingRight = true;
            else if (h * speed < 0)
                isFacingRight = false;
            // flips sprite dependant on direction
            if (isFacingRight)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
                transform.localScale = new Vector3(-1f, 1f, 1f);

        }
        // reset for later iterations  
        isSprinting = false;

    }

    // used for shooting fire balls
    void Fire()
    {
        // create fireball, and give physics
        GameObject go = (GameObject) Instantiate(m_FireBall, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
        // play animations and sound
        animator.Play("fireShooting");
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
        // start delay before player can shoot again
        StartCoroutine(CanShoot(cooldown));
    }

    // used for tail swiping
    void Swipe()
    {
        // turns on box collider
        GameObject.Find("Player(Clone)/Tail").GetComponent<BoxCollider2D>().enabled = true;
        // play animations / sound
        animator.Play("tailSwipe");
        m_ShootingAudio.clip = m_TailSwipe;
        m_ShootingAudio.Play();
        
        // start coroutine for delay
        StartCoroutine(CanShoot(0.25f));

    }

    //used for flying
    void marioFly()
    {
        float flyForce = 10f;
        actualFlight = true;
        flying = false;
        jumpCount = 0;
        rigidbody.AddForce(new Vector2(flyForce,20f));
        animator.Play("leafFlying");
        rigidbody.gravityScale = 0.5f;
        flightCamera = true;

        StartCoroutine(CanShoot(2f));
        rigidbody.gravityScale = 0.75f;



        // allow addforce while falling
        // play tail wag animation

    }


    // coroutine for delay on shooting / swiping / jumping
    IEnumerator CanShoot(float seconds)
    {
        if(actualFlight)
        {
            jumpCount++;
        }
        canShoot = false;
        yield return new WaitForSeconds(seconds);
        canShoot = true;
        GameObject.Find("Player(Clone)/Tail").GetComponent<BoxCollider2D>().enabled = false;
    }

}


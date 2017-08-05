using UnityEngine;
using System.Collections;

public class PlayerPickup : MonoBehaviour {

    private GameManager gm;         // used for updating other stats
    public Animator anim;           // used for changing the animation weight for different states
    private PlayerHealth stats;     // used for checking if mario has a star or gets a star
    private Vector3 offset = new Vector3(0f, -0.04f, 0f); // offset for moving box collider of groundCheck
    private Transform groundCheck;  // groundCheck used for updating position of box collider
                                    //  private Rigidbody2D rb2d;
    float fatBox;
    float originalSize;


    public AudioSource powerUpAudio;
    public AudioClip p_powerUp;
    public AudioClip m_Coin;
    private int originalState;

    // Use this for initialization
    void Awake () {
        groundCheck = transform.Find("GroundCheck");
        stats = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //       rb2d = GetComponent<Rigidbody2D>();

  //      fatBox = GameObject.Find("Player").GetComponent<BoxCollider2D>().size.y; 
        originalSize = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size.y;
        setAnimator(gm.PlayerState, 1);
    }
	
	// Update is called once per frame
	void Update () {
        originalState = gm.PlayerState;
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
       
        switch(other.gameObject.tag)
        {
            case "mushroom":
                // if the player is already large, just add to score
                if(gm.PlayerState >= 2)
                {
                    pickupScore(false);
                }
                // else make the player bigger and add to score
                else
                {
                    gm.PlayerState = 2;
                    setAnimator(gm.PlayerState, originalState);
                    playPickUpSounds();                  
                    pickupScore(false);
                }
                Destroy(other.gameObject);
                break;
            case "fireflower":
                if (gm.PlayerState < 3)
                {
                    gm.PlayerState = 3;
                    playPickUpSounds();
                    setAnimator(gm.PlayerState, originalState);
                    pickupScore(false);
                }
                else
                    pickupScore(false);

                Destroy(other.gameObject);
                break;
            case "leaf":
                if(gm.PlayerState != 4)
                {
                    gm.PlayerState = 4;
                    playPickUpSounds();
                    setAnimator(gm.PlayerState, originalState);
                    pickupScore(false);
                }else
                {
                    pickupScore(false);
                }
                Destroy(other.gameObject);
                break;

            case "Star":
                stats.isStar = true;
                Destroy(other.gameObject);
                break;
            case "1UP":
                pickupScore(false);
                gm.PlayerLives++;
                Destroy(other.gameObject);
                break;
            case "Coin":
                pickupScore(true);
                gm.Coins++;
                other.gameObject.GetComponent<Animator>().Play("pickedUp");
                Destroy(other.gameObject);
                powerUpAudio.clip = m_Coin;
                powerUpAudio.Play();
                break;
            default:
                break;
        }
    }
    // checks if coin and adds to score
    void pickupScore(bool isCoin)
    {
        if (!isCoin)
        {
            gm.Score += 1000;
        }
        else
            gm.Score += 100;
    }

    // animation and simple changes to colliders based off of new size
    public void setAnimator(int state, int ogState)
    {
        switch(state)
        {
            case 1:
                //small state
                if (ogState != 1)
                {
                    transform.Find("point1").position -= new Vector3(0f, -0.0528f, 0f);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size += new Vector2(0f, -0.09f);
                    GameObject.Find("Player(Clone)/base").transform.position -= new Vector3(0f, -0.05f, 0f);
                }
                fatBox = originalSize;
                clearWeights();
                break;
            case 2:
                clearWeights();
                if (ogState == 1)
                {
                    transform.Find("point1").position += new Vector3(0f, -0.0528f, 0f);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size += new Vector2(0f, 0.09f);
                    GameObject.Find("Player(Clone)/base").transform.position += new Vector3(0f, -0.05f, 0f);
                }
                anim.SetLayerWeight(1, 1f);
                
                //big state
                break;
            case 3:
                clearWeights();
                if(ogState == 1)
                {
                    transform.Find("point1").position += new Vector3(0f, -0.0528f, 0f);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size += new Vector2(0f, 0.09f);
                    GameObject.Find("Player(Clone)/base").transform.position += new Vector3(0f, -0.05f, 0f);
                }
                anim.SetLayerWeight(2, 1f);
               
                break;
            case 4:
                // moves the transform up in position if it isnt already done.
                if (ogState == 1)
                {
                    transform.Find("point1").position += new Vector3(0f, -0.0528f, 0f);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size += new Vector2(0f, 0.09f);
                    GameObject.Find("Player(Clone)/base").transform.position += new Vector3(0f, -0.05f, 0f);
                }
                clearWeights();
                anim.SetLayerWeight(3, 1f);
                break;
                   
        }

    }

    public void playPickUpSounds()
    {        
        powerUpAudio.clip = p_powerUp;
        powerUpAudio.Play();
    }

    // used to help change dudes.
    void clearWeights()
    {
        for (int i = 0; i < 4; i++)
        {
            anim.SetLayerWeight(i, 0f);
        }
    }

    // check if this works????
    //IEnumerator powerUpPause(float seconds)
    //{
    //    rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    //    yield return new WaitForSeconds(seconds);
    //    rb2d.constraints = RigidbodyConstraints2D.None;
    //    rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    //}
}

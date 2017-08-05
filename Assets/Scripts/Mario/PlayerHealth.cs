using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    // public for now, will change once fully set up
    // player state 1 mini, 2 big, 3 raccoon, 4 fire, 5 star
    private int holder;
    public bool isStar = false;
    [SerializeField]
    public bool canBeDamaged = true;

    PlayerPickup stateMachine;
    GameManager gm;
    Animator animator;
    Rigidbody2D rb2d;

    // Audio Sources.
    public AudioSource m_MovementAudio;
    public AudioClip m_dieClip;
    public AudioClip m_powerDown;
    int originalState = 0;
    // public Text lifeText;  // Used later on for text implementation
    // public Text scoreText;  // Used later on for text implementation


    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<PlayerPickup>();
        
        
    }


    void setAnimator()
    {

    }



    public void isDamaged()
    {
        if (canBeDamaged && !isStar)
        {
            switch (gm.PlayerState)
            {
                case 1:
                    gm.PlayerState = 0;
                    checkDead();
                    break;
                case 2:
                    gm.PlayerState = 1;
                    sfxStateChange();
                    break;
                case 3:
                    gm.PlayerState = 2;
                    sfxStateChange();
                    break;
                case 4:
                    gm.PlayerState = 2;
                    sfxStateChange();
                    break;
            }
            StartCoroutine(recentDamage());
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Enemy") && isStar)
        {
            Destroy(other.gameObject);
        }

    }

    void sfxStateChange()
    {
        m_MovementAudio.clip = m_powerDown;
        m_MovementAudio.Play();
        stateMachine.setAnimator(gm.PlayerState, originalState);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            gm.PlayerState = 0;
            // boxcollider turning off
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            checkDead();
        }
        if (other.gameObject.CompareTag("circleOfDeath"))
        {
            originalState = gm.PlayerState;
            isDamaged();
            StartCoroutine(recentDamage());
        }
    }





    void checkDead()
    {
        if (gm.PlayerState == 0)
        {
            onDeath();
            Debug.Log("INSIDE LAVA HELP");
            gm.PlayerLives--;
            gm.PlayerState = 1;
            gm.StartTimer = false;
            gm.EndTimer = true;

            
            if(!gm.checkGameOver())
                Invoke("Reset", 2.5f);
            // LoadScene back to WorldMap flag based off current level?
        }
    }

	
    void onDeath()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("isDead", true);
        m_MovementAudio.clip = m_dieClip;
        m_MovementAudio.Play();
     }


    void Reset()
    {
        SceneManager.LoadScene("World Map");
    }

	// Update is called once per frame
	void Update () {
	
        if(isStar)
        {
            StartCoroutine(starImmunity());
        }

	}

    IEnumerator recentDamage()
    {
        canBeDamaged = false;
    //    gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        canBeDamaged = true;
  //      gameObject.GetComponent<BoxCollider2D>().enabled = true;

    }


    IEnumerator starImmunity()
    {
        // play sound

        // give immunity / take away
        animator.SetBool("isStar", true);
        animator.Play("star");
        yield return new WaitForSeconds(5f);
        animator.SetBool("isStar", false);  
        isStar = false;
    }

    IEnumerator death()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1;
    }


}

using UnityEngine;
using System.Collections;
public class bossMove : MonoBehaviour
{
    PlayerHealth damage;
    public Transform weakness;
    castleManager cm;
    Animator anim;
    float direction = -1f;
    float speed = 0.5f;
    int life = 3;
    Rigidbody2D rgbd2D;
    bool isSpiky = false;
    bool canbeDamaged = true;
    bool canJump = true;
    // Use this for initialization
    void Start()
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        rgbd2D = GetComponent<Rigidbody2D>();
        cm = GameObject.Find("LevelManager").GetComponent<castleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime * direction, 0f, 0f));

        if(life == 3)
        {
            StartCoroutine(delay());
        }


        if (life == 2 && canJump)
        {
            speed = 0.5f;
            bossJump();
            StartCoroutine(delay());
        }

        if(life == 1)
        {
            speed = 2.5f;
        }


    }
    void OnCollisionEnter2D(Collision2D other)
    {

        float height = other.contacts[0].point.y - weakness.position.y;
        if (other.gameObject.tag == "Player" && canbeDamaged)
        {

            if (height > 0)
            {
                if (life == 0)
                {
                    anim.SetBool("isDead", true);
                    cm.BossDead = true;
                    Destroy(gameObject, 1f);
                }
                else if(life == 3 && isSpiky)
                {
                    damage.isDamaged();
                }
                else
                {
                    --life;
                    other.rigidbody.AddForce(new Vector2(0, 1f));
                    StartCoroutine(dmgDelay());
                }

            }
            else
               damage.isDamaged();

        }
        if (other.gameObject.tag == "Wall")
        {
            direction *= -1;
            if (life < 3)
            {
               
                speed = 1f;        
                rgbd2D.AddForce(new Vector2(speed * direction, 2.5f));
                anim.Play("jumping");
            }
        }
        // if it hits the camera, or falls into lava
        //if (other.gameObject.tag == "MainCamera" || other.gameObject.tag == "Lava")
        //    Destroy(gameObject);
    }
    void bossJump()
    {
        rgbd2D.AddForce(new Vector2(speed, 2.5f));
    }

    IEnumerator spikeDelay()
    {
        anim.SetBool("isSpikeForm", true);
        isSpiky = true;
        yield return new WaitForSeconds(2f);
        anim.SetBool("isSpikeForm", false);
        isSpiky = false;
    }

    IEnumerator dmgDelay()
    {
        canbeDamaged = false;
        yield return new WaitForSeconds(2f);
        canbeDamaged = true;
    }

    IEnumerator delay()
    {
        
        yield return new WaitForSeconds(1f);
    }
}

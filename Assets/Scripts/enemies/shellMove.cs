using UnityEngine;
using System.Collections;

public class shellMove : MonoBehaviour {

    bool isMoving = false;
    bool dmgPlayer = true;
    public Transform weakness;
    Animator anim;
    PlayerHealth damage;
    public float speed = 2.5f;
    float direction;
    bool interactable = true;
    float height;


	// Use this for initialization
	void Start () {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        direction = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
            transform.Translate(new Vector3(speed * Time.deltaTime * direction, 0f, 0f));
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        // if shell is sitting still and able to be kicked
        if (!isMoving && interactable)
        {
            // kick the shell
            if(other.gameObject.tag == "Player")
            {
                // decide which side kicking from
                if (other.gameObject.transform.position.x < transform.position.x)
                {
                    direction = 1f;
                    transform.Translate(new Vector3(speed * Time.deltaTime * direction, 0f, 0f));
                }
                else
                {
                    direction = -1f;
                    transform.Translate(new Vector3(speed * Time.deltaTime * direction, 0f, 0f));
                }
                isMoving = true;
                anim.SetBool("isSpinning", true);
                StartCoroutine(canDamage());
            }
            // potentially respawn
        }
        //
        if(isMoving)
        {
            // if the shell is moving and hits enemy
            if (other.gameObject.tag == "Enemy")
            {
                Destroy(other.gameObject);
            }
            // if it hits the camera border
            if (other.gameObject.tag == "MainCamera" || other.gameObject.tag == "Lava")
            {
                Destroy(gameObject);
            }
            // if it hits a wall or another shell, turn around
            if(other.gameObject.tag == "goldBrick")
            {
                direction *= -1;
                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "Wall" || other.gameObject.tag == "shell" || other.gameObject.tag == "questionBlock")
            {
                direction *= -1;
            }
        }
        if(isMoving && dmgPlayer)
        {
            // if player is attempting to jump on the shell
            height = other.contacts[0].point.y - weakness.position.y;
            if (other.gameObject.tag == "Player")
            {
               if (height > 0)
               {
                   isMoving = false;
                    anim.SetBool("isSpinning", false);
                   other.rigidbody.AddForce(new Vector2(0, 100));
                   StartCoroutine(haltedShell());
               }
               else
                   damage.isDamaged();
            }
        }

        if (other.gameObject.tag == "MainCamera")
            Destroy(gameObject);
    }
    // to postpone moving the shell when stopped
    IEnumerator haltedShell()
    {
        interactable = false;
        yield return new WaitForSeconds(0.25f);
        interactable = true;
    }
    // if you can damage the player
    IEnumerator canDamage()
    {
        dmgPlayer = false;
        yield return new WaitForSeconds(0.25f);
        dmgPlayer = true;
    }

}

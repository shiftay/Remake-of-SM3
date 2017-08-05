using UnityEngine;
using System.Collections;

public class goombaMove : MonoBehaviour {

    PlayerHealth damage;

    public Transform weakness;
    Animator anim;
    float direction;

    public float Direction
    {
        get { return direction; }
        set { direction = value; }
    }

	// Use this for initialization
	void Start () {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(new Vector3(0.65f * Time.deltaTime * direction, 0f, 0f));
	}


    void OnCollisionEnter2D (Collision2D other)
    {
        float height = other.contacts[0].point.y - weakness.position.y;
        if (other.gameObject.tag == "Player")
        {
            if (damage.isStar)
            {
                death();
            }
            else
            {
                if (height > 0)
                {
                    death();
                    other.rigidbody.AddForce(new Vector2(0, 45f));
                }
                else
                    damage.isDamaged();
            }
        }

        if(other.gameObject.tag == "Wall")
        {
            direction *= -1;
        }

        // if it hits the camera, or falls into lava
        if (other.gameObject.tag == "MainCamera" || other.gameObject.tag == "Lava")
            Destroy(gameObject);

    }

    void death()
    {
        anim.SetBool("isSquished", true);
        Destroy(gameObject, 0.5f);
        gameObject.tag = "deadEnemy";
    }

}

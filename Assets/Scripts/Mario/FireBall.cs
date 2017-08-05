using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {
        
    Vector3 originalPosition;   //  original position of player
    public float difference;    //  difference between current pos and original
    float xoffset;       //  offset 
    Rigidbody2D ball;           //  for adding velocity, making it "jump"

    void Start()
    {
        originalPosition = transform.position;
        ball = GetComponent<Rigidbody2D>();
    }
    // deletes a ball when its gone a certain difference
    void Update()
    {
        difference =  transform.position.x - originalPosition.x;
      
        if ( difference > 3 || difference < -3)
        {
            deleteFire();
        }
    }

    // on colision enter
    void OnCollisionEnter2D(Collision2D other)
    {
        // if he is looking the opposite direction makes negative for velocity
        if (transform.position.x < originalPosition.x)
            difference = -1;


        switch(other.gameObject.tag)
        {
            case "Enemy":
                Destroy(other.gameObject);
                deleteFire();
                break;
            case "Ground":
                ball.velocity = new Vector2(2f * Mathf.Sign(difference), 2f);
                break;
            case "Wall":
                deleteFire();
                break;
            case "MainCamera":
                deleteFire();
                break;
            default:
                deleteFire();
                break;
        }



        // if it hits the ground bounce
        if (other.gameObject.tag == "Ground")
            ball.velocity = new Vector2(2f * Mathf.Sign(difference), 2f);
        // if it hits an enemy kill enemy and kill fireball
        else if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            deleteFire();
        }
        else if (other.gameObject.tag == "Wall" || other.gameObject.tag == "MainCamera")
        {
            deleteFire();
        }

    }

    // delete the fireball
    void deleteFire()
    {
        Destroy(gameObject);
        // can add an animation for it exploding and play it here.
    }

}

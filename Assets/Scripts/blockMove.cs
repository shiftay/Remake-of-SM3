using UnityEngine;
using System.Collections;

public class blockMove : MonoBehaviour {

    Rigidbody2D rb2d;
    GameObject player;

    float distBetween;
    float fallDelay = 1.5f;
    float direction = -1f;

    bool canMove = false;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();


    }
	
	// Update is called once per frame
	void Update () {

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            // do nothing
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }


        distBetween = transform.position.x - player.transform.position.x;

        if(distBetween < 2.5f)
        {
            canMove = true;
        }

        if (canMove)
            startMove();

    }

    void startMove()
    {
        transform.Translate(new Vector3(0.5f * Time.deltaTime * direction, 0f, 0f));
   //     rb2d.velocity = new Vector3(10f * Time.deltaTime * direction, 0f, 0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canMove = false;
            Invoke("Fall", fallDelay);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Lava")
            Destroy(gameObject);
    }


    void Fall()
    {
        rb2d.isKinematic = false;
    }
}

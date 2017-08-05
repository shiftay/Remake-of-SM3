using UnityEngine;
using System.Collections;

public class starMovement : MonoBehaviour {
    
    Vector3 originalPosition;   //  original position of player
    public float difference;    //  difference between current pos and original
    float xoffset;       //  offset 
    Rigidbody2D ball;



    // Use this for initialization
    void Start () {
        originalPosition = transform.position;
        ball = GetComponent<Rigidbody2D>();
        ball.velocity = new Vector2(1.2f * Mathf.Sign(difference), 2f);
    }
	
	// Update is called once per frame
	void Update () {
        difference = transform.position.x - originalPosition.x;
        
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
            ball.velocity = new Vector2(1.2f * Mathf.Sign(difference), 2f);

        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "MainCamera")
        {
            Destroy(gameObject);
        }
    }

}

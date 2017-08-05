using UnityEngine;
using System.Collections;

public class pickUpMovement : MonoBehaviour {

    private GameObject player;
    private float startingDirection;
    private Vector3 walking;
    public float speed = 0.75f;
    private float originalDiff;
    private bool canMove = false;
 

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
     
            
        if (player.transform.position.x < transform.position.x)
            startingDirection = 1f;
        else
            startingDirection = -1f;

	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(sweeper());

        if (canMove)
        {
            walking.x = speed * startingDirection * Time.deltaTime;
            transform.Translate(walking);
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            if (startingDirection > 0)
                startingDirection = -1f;
            else
                startingDirection = 1f;
        }
    }



    IEnumerator sweeper()
    {
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }
}

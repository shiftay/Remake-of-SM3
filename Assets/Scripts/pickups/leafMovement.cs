using UnityEngine;
using System.Collections;

public class leafMovement : MonoBehaviour {

    Vector3 walking;
    float speed = 1f;
    float startingDirection = 1f;
    Vector3 startingPos;
    public float difference;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        difference = transform.position.x - startingPos.x;

        if (difference > 0.4f)
            startingDirection = -1f;
        else if (difference < -0.4f)
            startingDirection = 1f;

        walking.x = speed * startingDirection * Time.deltaTime;
        transform.Translate(walking);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Lava")
            Destroy(gameObject);
    }
}

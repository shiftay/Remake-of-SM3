using UnityEngine;
using System.Collections;

public class secretBox : MonoBehaviour {

    Rigidbody2D contentsSpawn;
    public GameObject contents;
    private Vector2 offset = new Vector2(0f, .15f);


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "RaccoonTail")
        {
            contentsSpawn = Instantiate(contents, (Vector2)transform.position + offset, Quaternion.identity) as Rigidbody2D;


            if (other.transform.position.x > transform.position.x)
            {
                transform.Translate(new Vector3(-0.15f, 0f));
                transform.Translate(new Vector3(0.15f, 0f));
            }
            else
            {
                transform.Translate(new Vector3(0.15f, 0f));
                transform.Translate(new Vector3(-0.15f, 0f));
            }
        }

    }


}

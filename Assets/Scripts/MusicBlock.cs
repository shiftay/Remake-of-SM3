using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicBlock : MonoBehaviour {


    public bool dropsItem = false;
    public Transform middle;
    public GameObject contents;
    public bool specialBlock = true;
    Vector2 offset = new Vector2(0f, 0.15f);
    GameManager gm;

    // Use this for initialization
    void Start()
    {
        // no idea what this is for currently
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
	}
    
    void OnCollisionEnter2D(Collision2D other)
    {
        float height = other.contacts[0].point.y - middle.position.y;
        if (other.gameObject.tag == "Player")
        {
            // do some stuff if player hits the block, bounce them?
            other.rigidbody.AddForce(new Vector2(0f, 15f));
        }

        // if block is special
        if (height > 0)
            offset *= -1;
        

        if(other.gameObject.tag == "Player" && dropsItem)
        {
            GameObject go = (GameObject)Instantiate(contents, (Vector2)transform.position + offset, Quaternion.identity);
        }

    }


    void specialTrigger()
    {
        // spawn other special level?
    }



}

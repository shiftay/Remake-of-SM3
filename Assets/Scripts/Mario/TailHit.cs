using UnityEngine;
using System.Collections;

public class TailHit : MonoBehaviour {

   

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }


	// Use this for initialization
	void Awake () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

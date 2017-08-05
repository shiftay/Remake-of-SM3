using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class minimapThing : MonoBehaviour {

    public bool buttonPressed = false;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetButtonDown("Jump"))
        //    buttonPressed = true;


	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {

            if(Input.GetButtonDown("Jump"))
            {
                Debug.Log("inside jump");
                SceneManager.LoadScene(gameObject.name);
            }
        }
    }



}

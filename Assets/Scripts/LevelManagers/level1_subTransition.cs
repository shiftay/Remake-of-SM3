using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class level1_subTransition : MonoBehaviour {

    GameManager gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            gm.LeftSub1 = true;
            SceneManager.LoadScene("level1");
        }
    }



}

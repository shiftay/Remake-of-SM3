﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class level5_endTransition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("level5_final");
        }

    }


}

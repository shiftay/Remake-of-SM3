using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class level1_5subTransition : MonoBehaviour {

    GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.LeftSub5 = true;
            SceneManager.LoadScene("level5");
        }
    }



}

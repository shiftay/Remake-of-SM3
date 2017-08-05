using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class level2_subTransition : MonoBehaviour {

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
            gm.LeftSub2 = true;
            SceneManager.LoadScene("level2");
        }
    }
}

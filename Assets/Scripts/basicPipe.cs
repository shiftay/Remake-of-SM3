using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class basicPipe : MonoBehaviour {

    GameManager gm;
    GameObject player;

    // Use this for initialization
    void Start() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {

        if (gm.Teleport)
        {
            // teleport to another map
            switch (SceneManager.GetActiveScene().name)
            {
                case "level1":
                    SceneManager.LoadScene("level1_sub");
                    setBools();
                    break;
                case "level2":
                    SceneManager.LoadScene("level2_sub");
                    setBools();
                    break;
                case "level3":
                    SceneManager.LoadScene("level3_sub");
                    setBools();
                    break;
                case "level4":
                    player.transform.position = new Vector3(20.634f, -0.476f, 0f);
                    setBools();
                    break;
                case "level5":
                    SceneManager.LoadScene("level5_sub");
                    setBools();
                    break;
            }
        }

    }

    void setBools()
    {
        gm.Teleport = false;
        gm.OnPipe = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            gm.OnPipe = true;
        }
    }

}

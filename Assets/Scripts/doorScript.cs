using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class doorScript : MonoBehaviour {
    [SerializeField]
    float timer;
    string activeLevel;
    GameManager gm;




	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                Debug.Log(SceneManager.GetActiveScene().name);
                nextLevel();
            }
        }
    }

    void nextLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "castle":
                SceneManager.LoadScene("castle_sub");
                break;
            case "castle_sub":
                SceneManager.LoadScene("castle");
                gm.LeftCastleSub = true;
                break;
        }
    }


}

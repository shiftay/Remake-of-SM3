using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class castleManager : MonoBehaviour {

    public Transform[] marioSpawn;
    public GameObject playerPrefab;
    public GameObject doorPrefab;
    public GameObject bossPrefab;
    GameObject player;
    GameManager gm;
    bool canClose = true;
    bool bossDead = false;
    bool bosscanSpawn = true;
    [SerializeField]
    float timer;

    bool firstCompleted = false;

    public bool BossDead
    {
        get { return bossDead; }
        set { bossDead = value; }
    }



    // Use this for initialization
    void Start () {

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        gm.StartTimer = true;

        if (gm.LeftCastleSub)
        {
            player = (GameObject)Instantiate(playerPrefab, marioSpawn[1].position, Quaternion.identity);
            GameObject.Find("Main Camera").GetComponent<cameraManager>().StartPos = player.transform.position.x;
            GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 21.6f;

        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, marioSpawn[0].position, Quaternion.identity);
            GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 14.49f;
        
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(player.transform.position.x > 20.5 && canClose && bosscanSpawn)
        {
            GameObject door = (GameObject)Instantiate(doorPrefab, new Vector3(20.349f, -0.109f, 0f), Quaternion.identity);
            GameObject boss = (GameObject)Instantiate(bossPrefab, new Vector3(22.33f, -0.23f, 0f), Quaternion.identity);
            canClose = false;
            bosscanSpawn = false;
        }


        if(bossDead)
        {
            gm.EndTimer = true;

            gm.Score += (int)gm.Timer * 50;
            gm.StartTimer = false;

            gm.LevelCompleted = true;
            SceneManager.LoadScene("World Map");
            // move to worldmap
        }

	}

}

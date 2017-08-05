using UnityEngine;
using System.Collections;

public class Level1_4Manager : MonoBehaviour {

    public Transform marioSpawn;
    public GameObject playerPrefab;
    public GameObject rKoopaPrefab;
    public Transform rKoopaSpawn;
    GameObject player;
    GameManager gm;

    // Use this for initialization
    void Start () {
        player = (GameObject)Instantiate(playerPrefab, marioSpawn.position, Quaternion.identity);

        spawnRKoopa(rKoopaSpawn.position, 0.312f);
        // set end position
        GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 18.05f;
        GameObject.Find("Main Camera").GetComponent<cameraManager>().AutoScroller = true;
        GameObject.Find("Main Camera").GetComponentInChildren<EdgeCollider2D>().enabled = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.StartTimer = true;


    }

    // Update is called once per frame
    void Update () {

	}

    void spawnRKoopa(Vector2 sPoint, float distance)
    {
        GameObject go = (GameObject)Instantiate(rKoopaPrefab, sPoint, Quaternion.identity);
        go.GetComponent<rKoopaMove>().distance = distance;
    }
}

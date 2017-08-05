using UnityEngine;
using System.Collections;

public class level1_5subManager : MonoBehaviour {

    public GameObject playerPrefab;
    public Transform marioSpawn;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = (GameObject)Instantiate(playerPrefab, marioSpawn.position, Quaternion.identity);

        GameObject.Find("Main Camera").GetComponent<cameraManager>().AutoScroller = true;
        GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 7.396f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

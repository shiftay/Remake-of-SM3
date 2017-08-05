using UnityEngine;
using System.Collections;

public class castleSubManager : MonoBehaviour {

    public Transform marioSpawn;
    public GameObject playerPrefab;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = (GameObject)Instantiate(playerPrefab, marioSpawn.position, Quaternion.identity);

        // set end position
        GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 3.21f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

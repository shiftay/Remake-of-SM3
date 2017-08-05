using UnityEngine;
using System.Collections;

public class Level1_2subManager : MonoBehaviour {

    public Transform marioSpawn;
    public GameObject marioPrefab;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = (GameObject)Instantiate(marioPrefab, marioSpawn.position, Quaternion.identity);

        GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 0.07f;
    }

    // Update is called once per frame
    void Update () {
	
	}
}

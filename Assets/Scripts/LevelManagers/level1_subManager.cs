using UnityEngine;
using System.Collections;

public class level1_subManager : MonoBehaviour {

    public Transform marioSpawn;
    public GameObject marioPrefab;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = (GameObject)Instantiate(marioPrefab, marioSpawn.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

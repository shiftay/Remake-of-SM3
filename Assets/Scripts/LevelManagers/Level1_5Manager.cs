using UnityEngine;
using System.Collections;

public class Level1_5Manager : MonoBehaviour {
    GameManager gm;

    GameObject player;
    public GameObject playerPrefab;
    public GameObject beetlePrefab;
    public GameObject gKoopaPrefab;
    public Transform[] marioSpawn;
    public Transform[] gKoopaSpawn;
    public Transform[] beetleSpawn;
    bool canBeSpawned = true;
    float cameraDistance = 1.25f;
    float distBetween;


    // Use this for initialization
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (!gm.LeftSub5)
        {
            player = (GameObject)Instantiate(playerPrefab, marioSpawn[0].position, Quaternion.identity);
            GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 20.55556f;
            GameObject.Find("Main Camera").GetComponent<cameraManager>().World5 = true;
        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, marioSpawn[1].position, Quaternion.identity);
            GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 20.55556f;
            GameObject.Find("Main Camera").GetComponent<cameraManager>().World5 = true;
        }


        gm.StartTimer = true;

        spawnBeetle(beetleSpawn[0].position);
        spawnBeetle(beetleSpawn[1].position);


    }

    // Update is called once per frame
    void Update () {
        if (distance(gKoopaSpawn[0].position.x) >= cameraDistance - .05f && distance(gKoopaSpawn[0].position.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnGKoopa(gKoopaSpawn[0].position);
            spawnGKoopa(gKoopaSpawn[1].position);
            StartCoroutine(stopSpawning());
        }

        if (distance(gKoopaSpawn[2].position.x) >= cameraDistance - .05f && distance(gKoopaSpawn[2].position.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnGKoopa(gKoopaSpawn[2].position);
            StartCoroutine(stopSpawning());
        }

    }

    float distance(float x)
    {
        distBetween = x - player.transform.position.x;
        distBetween = Mathf.Round(distBetween * 100f) / 100f;
        return distBetween;
    }

    void spawnBeetle(Vector2 sPoint)
    {
        GameObject beetle = (GameObject)Instantiate(beetlePrefab, sPoint, Quaternion.identity);
    }

    void spawnGKoopa(Vector2 sPoint)
    {
        GameObject gKoopa = (GameObject)Instantiate(gKoopaPrefab, sPoint, Quaternion.identity);
    }

    IEnumerator stopSpawning()
    {
        canBeSpawned = false;
        yield return new WaitForSeconds(2f);
        canBeSpawned = true;
    }
}

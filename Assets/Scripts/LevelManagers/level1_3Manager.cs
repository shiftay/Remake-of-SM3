using UnityEngine;
using System.Collections;

public class level1_3Manager : MonoBehaviour {

    public GameObject rKoopaPrefab;
    public GameObject gKoopaPrefab;
    public GameObject goombaPrefab;
    public GameObject paraGoombaPrefab;
    public GameObject playerPrefab;
    public GameObject boomerPrefab;
    public Transform[] marioSpawn;
    Vector2 firstGK = new Vector2(0.739f, -0.78f);
    Vector2 secondGK = new Vector2(5.65f, -0.78f);
    Vector2 thirdGK = new Vector2(17.66f , -0.78f);

    Vector2 firstGmba = new Vector2(7.05f , -0.78f);
    Vector2 secondGmba = new Vector2(14.25f , -0.78f);
    Vector2 thirdGmba = new Vector2(14.75f , -0.78f);

    Vector2 firstRK = new Vector2(3.869f , -0.051f);
    Vector2 secondRK = new Vector2(14.62f , -0.01f);

    Vector2 firstBmr = new Vector2(2.158f, -0.71f);
    //Vector2 scndBmr = new Vector2();
    //Vector2 paraG_1 = new Vector2();



        
    //   public Transform[] enemies;
    GameManager gm;
    GameObject player;
    public AudioSource aSource;
    public AudioClip startClip;
    float distBetween;
    float cameraDistance = 1.25f;
    bool canBeSpawned = true;

    // Use this for initialization
    void Start()
    {
        aSource.clip = startClip;
        aSource.Play();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.StartTimer = true;

        if (!gm.LeftSub3)
        {
            player = (GameObject)Instantiate(playerPrefab, marioSpawn[0].position, Quaternion.identity);
            GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 22.223f;
        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, marioSpawn[1].position, Quaternion.identity);
            GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 22.223f;
        }

        spawnGKoopa(firstGK);
        spawnBoomer(firstBmr);
        spawnBoomer(new Vector2(11.76f, -0.71f));
        spawnRKoopa(firstRK, 0.311f);
    }
	
	// Update is called once per frame
	void Update() {

        // greenKoopa spawnPoints
        if (distance(thirdGK.x) >= cameraDistance - .05f && distance(thirdGK.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnGKoopa(thirdGK);
            StartCoroutine(stopSpawning());
        }
        // goombas
        if (distance(firstGmba.x) >= cameraDistance - .05f && distance(firstGmba.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnGoomba(firstGmba, -1);
            // need flying goomba
            StartCoroutine(stopSpawning());
        }
        if (distance(secondGmba.x) >= cameraDistance - .05f && distance(secondGmba.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnGoomba(secondGmba, -1);
            spawnGoomba(thirdGmba, -1);
            spawnRKoopa(secondRK, 0.306f);
            StartCoroutine(stopSpawning());
        }

        // redKoopa
        if (distance(firstRK.x) >= cameraDistance - .05f && distance(firstRK.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnGKoopa(secondGK);
            StartCoroutine(stopSpawning());

        }


    }
    // spawns red koopa, the distance is for how far to walk before turning
    // requires platform information spawn the koopa at the middle of the platform
    void spawnRKoopa(Vector2 sPoint, float distance)
    {
        GameObject rKoopa = (GameObject)Instantiate(rKoopaPrefab, sPoint, Quaternion.identity);
        rKoopa.GetComponent<rKoopaMove>().distance = distance;
    }

    //spawn green koopa
    void spawnGKoopa(Vector2 sPoint)
    {
        GameObject gKoopa = (GameObject)Instantiate(gKoopaPrefab, sPoint, Quaternion.identity);
    }

    //spawn boomerang thrower
    void spawnBoomer(Vector2 sPoint)
    {
        GameObject boomer = (GameObject)Instantiate(boomerPrefab, sPoint, Quaternion.identity);
    }

    // spawn paragoomba
    void spawnParaGoomba(Vector2 sPoint)
    {
        GameObject go = (GameObject)Instantiate(paraGoombaPrefab, sPoint, Quaternion.identity);
    }

    // spawn goomba
    void spawnGoomba(Vector2 sPoint, float direction)
    {
        GameObject goomba = (GameObject)Instantiate(goombaPrefab, sPoint, Quaternion.identity);
        goomba.GetComponent<goombaMove>().Direction = direction;
    }


    //distance between player and the spawn point of x
    float distance(float x)
    {
        distBetween = x - player.transform.position.x;
        distBetween = Mathf.Round(distBetween * 100f) / 100f;
        return distBetween;
    }

    // coroutine so multiples arent spawned, might need to slow down more
    IEnumerator stopSpawning()
    {
        canBeSpawned = false;
        yield return new WaitForSeconds(2f);
        canBeSpawned = true;
    }

}

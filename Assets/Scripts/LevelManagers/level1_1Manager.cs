using UnityEngine;
using System.Collections;

public class level1_1Manager : MonoBehaviour {

    GameManager gm;

    // setting mario instance
    public Transform[] marioSpawn;
    public GameObject marioPrefab;
    GameObject player;
    // attempt #1
    public Transform[] goomba;
    public Transform[] paragoomba;
    public Transform[] paraKoopa;
    public Transform[] gKoopa;

    float cameraDistance = 1.25f;

    // enemies
    public GameObject goombaPrefab;
    public GameObject rKoopaPrefab;
    public GameObject paraGoombaPrefab;
    public GameObject paraTroopaPreFab;
    public GameObject gKoopaPrefab;

    bool canBeSpawned = true;
    float distBetween;

	// Use this for initialization
	void Start () {

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(gm.LeftSub1)
            player = (GameObject)Instantiate(marioPrefab, marioSpawn[1].position, Quaternion.identity);
        else
            player = (GameObject)Instantiate(marioPrefab, marioSpawn[0].position, Quaternion.identity);

        // set camera max distance
        GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 25.69f;
        gm.StartTimer = true;


        //spawn goomba/redkoopa
        spawnGoomba(new Vector2(1.06f, -0.737f), -1f);
        spawnRKoopa(new Vector2(4.336f, -0.488f), 0.454f);
        spawnGoomba(new Vector2(4.799f, -0.737f), -1f);
	}
	
	// Update is called once per frame
	void Update () {


        if (distance(goomba[0].position.x) >= cameraDistance - .05f && distance(goomba[0].position.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnGoomba(goomba[0].position, -1f);
            spawnGoomba(goomba[1].position, -1f);
            spawnParaGoomba(paragoomba[0].position);
            StartCoroutine(stopSpawning());
        }

        if (distance(paraKoopa[0].position.x) >= cameraDistance - .05f && distance(paraKoopa[0].position.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnKTroopa(paraKoopa[0].position);
            spawnKTroopa(paraKoopa[1].position);
            spawnKTroopa(paraKoopa[2].position);
            spawnGKoopa(gKoopa[0].position);
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
    //spawn koopatroopa / parakoopa
    void spawnKTroopa(Vector2 sPoint)
    {
        GameObject go = (GameObject)Instantiate(paraTroopaPreFab, sPoint, Quaternion.identity);
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

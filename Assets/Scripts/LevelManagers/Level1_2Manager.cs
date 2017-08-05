using UnityEngine;
using System.Collections;

public class Level1_2Manager : MonoBehaviour {

    public GameObject goombaPrefab;
   // public GameObject pirPlantShootrPrefab;
    public GameObject paraGoombaPrefab;
    public Transform[] marioSpawn;
    public GameObject playerPrefab;
    GameObject player;
    GameManager gm;
    float cameraDistance = 1.25f;
    float distBetween;
    bool canBeSpawned = true;

    #region Vectors for spawning
    Vector2 gmbaPipe1 = new Vector2(2.85f, 0.02f); // right
    Vector2 gmbaPipe2 = new Vector2(5.25f, 0.107f); // right

    Vector2 firstParaR = new Vector2(7.63f, -0.272f); // left

    Vector2 gmbaDuo = new Vector2(11.424f, -0.381f); // left
    Vector2 gmbaDuo_1 = new Vector2(11.667f, 0.156f); // left

    Vector2 secondParaR = new Vector2(13.889f, -0.152f); // left

    Vector2 scndgmbaDuo = new Vector2(15.063f, -0.252f); //left
    Vector2 scndgmbaDuo_1 = new Vector2(15.414f, -0.352f);

    Vector2 startSpwngmba = new Vector2(17.95f, -0.67f);

    Vector2 gmbaPipe3 = new Vector2(20.235f, -0.02f); // right


    #endregion
    // Use this for initialization
    void Start () {

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(!gm.LeftSub2)
            player = (GameObject)Instantiate(playerPrefab, marioSpawn[0].position, Quaternion.identity);
        else
            player = (GameObject)Instantiate(playerPrefab, marioSpawn[1].position, Quaternion.identity);

        gm.StartTimer = true;

        GameObject.Find("Main Camera").GetComponent<cameraManager>().EndPos = 24.609f;

        spawnGoomba(startSpwngmba, -1);
        
    }
	
	// Update is called once per frame
	void Update () {


        if (distance(gmbaPipe1.x) >= cameraDistance - .05f && distance(gmbaPipe1.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            StartCoroutine(briefDelay());
            StartCoroutine(stopSpawning());
        }

        if (distance(gmbaPipe2.x) >= cameraDistance - .05f && distance(gmbaPipe2.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnGoomba(gmbaPipe2, 1);
            spawnParaGoomba(firstParaR, -1);
            StartCoroutine(stopSpawning());
        }
        if (distance(gmbaDuo.x) >= cameraDistance - .05f && distance(gmbaDuo.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnGoomba(gmbaDuo, -1);
            spawnGoomba(gmbaDuo_1, -1);
            StartCoroutine(stopSpawning());
        }

        if (distance(secondParaR.x) >= cameraDistance - .05f && distance(secondParaR.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnParaGoomba(secondParaR, -1);
            spawnGoomba(scndgmbaDuo, -1);
            spawnGoomba(scndgmbaDuo_1, -1);
            StartCoroutine(stopSpawning());
        }

        if (distance(gmbaPipe3.x) >= cameraDistance - .05f && distance(gmbaPipe3.x) <= cameraDistance + 0.05f && canBeSpawned)
        {
            spawnGoomba(gmbaPipe3, 1);
            StartCoroutine(stopSpawning());
        }



    }

    // check distance between objects
    float distance(float x)
    {
        distBetween = x - player.transform.position.x;
        distBetween = Mathf.Round(distBetween * 100f) / 100f;
        return distBetween;
    }


    void spawnGoomba(Vector2 position, float direction)
    {
        GameObject go = (GameObject)Instantiate(goombaPrefab, position, Quaternion.identity);
        go.GetComponent<goombaMove>().Direction = direction;
    }

    void spawnParaGoomba(Vector2 position, float direction)
    {
        GameObject go = (GameObject)Instantiate(paraGoombaPrefab, position, Quaternion.identity);
        go.GetComponent<RparaGoombaMove>().Direction = direction;
    }

    IEnumerator briefDelay()
    {
        spawnGoomba(gmbaPipe1, 1);
        yield return new WaitForSeconds(0.75f);
        spawnGoomba(gmbaPipe1, 1);
    }


    IEnumerator stopSpawning()
    {
        canBeSpawned = false;
        yield return new WaitForSeconds(2f);
        canBeSpawned = true;
    }

}

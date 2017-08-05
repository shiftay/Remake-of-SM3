using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class worldMapManager : MonoBehaviour {

    #region vars
    public GameObject[] levelPoints;
    public Sprite usedByMario;
    public Transform[] spawnPoints;
    public GameObject WMmario;
    private GameManager gm;
    
    Animator anim;
    [SerializeField]
    int lastCompleted = 0;

    public int LastCompleted
    {
        get { return lastCompleted; }
        set { lastCompleted = value; }
    }
    #endregion

    // Use this for initialization
    void Start () {

        Debug.Log(WMmario.transform.position);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GameObject.Find("castle").GetComponent<Animator>();
        GameObject go = (GameObject)Instantiate(WMmario, spawnPoints[gm.JustFinished].position, Quaternion.identity);

    }
	
	// Update is called once per frame
	void Update () {

        if(gm.Level1)
        {
            levelPoints[0].GetComponent<SpriteRenderer>().sprite = usedByMario;
        }
        if (gm.Level2)
        {
            levelPoints[1].GetComponent<SpriteRenderer>().sprite = usedByMario;
        }
        if (gm.Level3)
        {
            levelPoints[2].GetComponent<SpriteRenderer>().sprite = usedByMario;
        }
        if (gm.Level4)
        {
            levelPoints[3].GetComponent<SpriteRenderer>().sprite = usedByMario;
        }
        if (gm.Level5)
        {
            levelPoints[5].GetComponent<SpriteRenderer>().sprite = usedByMario;
        }
        // if castle is broken play animation. however will constantly be played
        // need a bool for only play once type of thing. 
        // TO DO: break door && setup blocking colliders + turn them off
        if (gm.Castle1)
            anim.Play("Mini Castle Animation");

	}

    public void isTrigger(Vector3 position)
    {
        // incase the press buttons not on spawns need to fix
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (position == spawnPoints[i].position)
            {
                SceneManager.LoadScene(spawnPoints[i].name);
            }
        }
    }
}

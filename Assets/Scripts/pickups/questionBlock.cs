using UnityEngine;
using System.Collections;

public class questionBlock : MonoBehaviour {
    // for spawning items
    Rigidbody2D contentsSpawn;
    // audio and contents of the block using prefabs
    public GameObject contents;
    public AudioSource b_aSource;
    public AudioClip b_bumpClip;
    public AudioClip b_coin;


    GameManager gm;
    Animator anim;

    public int coinCounter;
    [SerializeField]
    private bool isUsed = false;
    private Vector2 offset = new Vector2 (0f, .15f);
    int timesHit = 0;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    // Update is called once per frame
    void Update()
    {
        if (isUsed)
            anim.Play("UsedBlock");
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "shell")
        {

            // plays bump sound
            b_aSource.clip = b_bumpClip;
            b_aSource.Play();
            // if the block is used
            if (!isUsed)
            {
               
                switch (contents.tag)
                {
                    case "Coin":
                        //play animation/sound+ add score
                        timesHit++;
                        b_aSource.clip = b_coin;
                        b_aSource.Play();
                        gm.Coins++;
                        gm.Score += 100;
                        if(timesHit == coinCounter)
                            isUsed = true;
                        break;
                    case "1UP":
                        contentsSpawn = Instantiate(contents, (Vector2)transform.position + offset, Quaternion.identity) as Rigidbody2D;
                        isUsed = true;
                        break;
                    case "mushroom":
                        contentsSpawn = Instantiate(contents, (Vector2)transform.position + offset, Quaternion.identity) as Rigidbody2D;
                        isUsed = true;
                        anim.Play("UsedBlock");
                        break;
                    case "leaf":
                        contentsSpawn = Instantiate(contents, (Vector2)transform.position + offset, Quaternion.identity) as Rigidbody2D;
                        anim.Play("UsedBlock");
                        isUsed = true;
                        break;
                    case "fireflower":
                        contentsSpawn = Instantiate(contents, (Vector2)transform.position + offset, Quaternion.identity) as Rigidbody2D;
                        anim.Play("UsedBlock");
                        isUsed = true;
                        break;
                    case "Star":
                        // LOGIC FOR JUMPING STAR
                        contentsSpawn = Instantiate(contents, (Vector2)transform.position + offset, Quaternion.identity) as Rigidbody2D;
                        isUsed = true;
                        anim.Play("UsedBlock");
                        break;
                }
            }
        }
    }




}

using UnityEngine;
using System.Collections;

public class boomerScript : MonoBehaviour {

    public GameObject boomerang;
    public Transform weakness;

    PlayerHealth damage;
    Animator anim;

    float direction = -1f;
    Vector3 startPos;
    float distFromStart;
    bool canBeDamaged = true;
    bool canThrow = true;
    Vector3 offset = new Vector3(0.001f, 0f, 0f);
    GameObject character;


	// Use this for initialization
	void Start () {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        transform.Translate(new Vector3(0.2f * direction * Time.deltaTime, 0f, 0f));
        character = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        distFromStart = transform.position.x - startPos.x;

        if (Mathf.Abs(distFromStart) > 0.2f)
            direction *= -1;

        transform.Translate(new Vector3(0.2f * direction * Time.deltaTime, 0f, 0f));

        

        if(canThrow)
        {
            Invoke("throwBoomerang", 0.5f);
            StartCoroutine(delayThrow());
            
        }

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        float height = other.contacts[0].point.y - weakness.position.y;

        if (other.gameObject.tag == "Player" && canBeDamaged)
        {
            if (damage.isStar)
            {
                death();
            }
            else
            {
                if (height > 0)
                {
                    other.rigidbody.AddForce(new Vector2(0, 150));
                    death();
                }
                else
                    damage.isDamaged();
            }
        }

        if (other.gameObject.tag == "MainCamera")
            Destroy(gameObject);

    }


    void death()
    {
        Destroy(gameObject);
        gameObject.tag = "deadEnemy";
    }

    void throwBoomerang()
    {
        GameObject go = (GameObject)Instantiate(boomerang, transform.position + offset, Quaternion.identity);
        anim.SetBool("isThrowing", true);
        go.GetComponent<boomerrangMove>().StartPos = transform.position + offset;
        StartCoroutine(throwingAnim());
    }

    IEnumerator throwingAnim()
    {
        yield return new WaitForSeconds(1.05f);
        anim.SetBool("isThrowing", false);
    }


    // based off of how the time on the boomerang, delay HAS to change
    IEnumerator delayThrow()
    {
        canThrow = false;
        yield return new WaitForSeconds(2.5f);
        canThrow = true;
    }
}

using UnityEngine;
using System.Collections;

public class RparaGoombaMove : MonoBehaviour {

    Rigidbody2D pgoombaB;
    float difference = -1f;
    PlayerHealth damage;
    public GameObject m_goomba;
    public Transform weakness;
    bool canJump = true;

    public float Direction
    {
        get { return difference; }
        set { difference = value; }
    }




    // Use this for initialization
    void Start()
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        pgoombaB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (difference > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            transform.localScale = new Vector3(1f, 1f, 1f);


        if (canJump)
        {
            pgoombaB.AddForce(new Vector2(1f, 150f));
            StartCoroutine(delayJump());
        }
        else
            transform.Translate(new Vector3(0.5f * difference * Time.deltaTime, 0f, 0f));

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        float height = other.contacts[0].point.y - weakness.position.y;


            

        if (other.gameObject.tag == "Player")
        {
            if (damage.isStar)
            {
                death();
            }
            else
            {
                if (height > 0)
                {
                    makeGoomba();
                    other.rigidbody.AddForce(new Vector2(0, 150));
                }
                else
                    damage.isDamaged();
            }
        }

        if (other.gameObject.tag == "Wall")
            difference *= -1f;

        if (other.gameObject.tag == "MainCamera" || other.gameObject.tag == "Lava")
            Destroy(gameObject);



    }


    IEnumerator delayJump()
    {
        canJump = false;
        yield return new WaitForSeconds(1f);
        canJump = true;
    }

    void makeGoomba()
    {
        GameObject go = (GameObject)Instantiate(m_goomba, transform.position, Quaternion.identity);
    //    go.GetComponent<koopaMove>().CanBeDamaged = false;
        go.GetComponent<goombaMove>().Direction = difference;
        Destroy(gameObject);
    }


    void death()
    {
        Destroy(gameObject);
        gameObject.tag = "deadEnemy";
    }
}

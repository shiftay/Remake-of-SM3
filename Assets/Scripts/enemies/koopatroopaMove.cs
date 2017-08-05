using UnityEngine;
using System.Collections;

public class koopatroopaMove : MonoBehaviour {

    Rigidbody2D koopaT;
    float difference = -1f;
    PlayerHealth damage;
    public GameObject m_koopa;
    public Transform weakness;

	// Use this for initialization
	void Start () {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        koopaT = GetComponent<Rigidbody2D>();
        koopaT.velocity = new Vector2(0.5f * difference, 2.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (difference > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        float height = other.contacts[0].point.y - weakness.position.y;

        if (other.gameObject.tag == "Ground")
            koopaT.velocity = new Vector2(0.5f * Mathf.Sign(difference), 2.5f);

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
                    makeKoopa();
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




    void makeKoopa()
    {
        GameObject go = (GameObject)Instantiate(m_koopa, transform.position, Quaternion.identity);
        go.GetComponent<koopaMove>().CanBeDamaged = false;
        go.GetComponent<koopaMove>().Direction = difference;
        Destroy(gameObject);
    }


    void death()
    {
        Destroy(gameObject);
        gameObject.tag = "deadEnemy";
    }
}

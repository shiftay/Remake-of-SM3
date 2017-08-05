using UnityEngine;
using System.Collections;

public class koopaMove : MonoBehaviour {
    Animator anim;
    PlayerHealth damage;
    float direction = -1f;
    public Transform weakness;
    public GameObject m_shell;
    bool canBeDamaged = true;
    #region Mutators   
    public bool CanBeDamaged
    {
        get { return canBeDamaged; }
        set { canBeDamaged = value; }
    }

    public float Direction
    {
        get { return direction; }
        set { direction = value; }
    }
    #endregion
    // Use this for initialization
    void Start()
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canBeDamaged)
            StartCoroutine(recentDamage());

        if (direction > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if(direction < 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        transform.Translate(new Vector3(0.5f * direction * Time.deltaTime, 0f, 0f));
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
                    makeShell();
                    other.rigidbody.AddForce(new Vector2(0, 150));
                }
                else
                    damage.isDamaged();
            }
        }

        if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
        {
            direction *= -1;
        }

        if (other.gameObject.tag == "MainCamera" || other.gameObject.tag == "Lava")
            Destroy(gameObject);


    }

    void death()
    {
        Destroy(gameObject);
        gameObject.tag = "deadEnemy";
    }

    void makeShell()
    {
        GameObject go = (GameObject)Instantiate(m_shell, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator recentDamage()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(0.5f);
        canBeDamaged = true;
    }
}
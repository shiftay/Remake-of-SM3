using UnityEngine;
using System.Collections;

public class rKoopaMove : MonoBehaviour {

    public GameObject m_rShell;
    public Transform weakness;

    Animator anim;
    PlayerHealth damage;
    Vector3 startPos;

    bool canBeDamaged = true;
    public float distFromStart;
    float direction = 1f;
    public float distance;
    bool isGoingLeft = false;
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

    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }
    #endregion
    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!canBeDamaged)
            StartCoroutine(recentDamage());

        distFromStart = transform.position.x - startPos.x;

        if (direction > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (direction < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (isGoingLeft)
        {
            // If gone too far, switch direction
            if (distFromStart < -distance)
                SwitchDirection();

            transform.Translate(new Vector3(0.5f * direction * Time.deltaTime, 0f, 0f));
        }
        else
        {
            // If gone too far, switch direction
            if (distFromStart > distance)
                SwitchDirection();

            transform.Translate(new Vector3(0.5f * direction * Time.deltaTime, 0f, 0f));
        }
    }

    // switches the direction it goes
    void SwitchDirection()
    {
        isGoingLeft = !isGoingLeft;
        direction *= -1;
    }

    // on collision enter
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

        if (other.gameObject.tag == "MainCamera")
            Destroy(gameObject);
    }

    // destroy the object
    void death()
    {
        Destroy(gameObject);
        gameObject.tag = "deadEnemy";
    }

    // make a new red shell
    void makeShell()
    {
        GameObject go = (GameObject)Instantiate(m_rShell, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // checks if player can be damaged
    IEnumerator recentDamage()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(0.5f);
        canBeDamaged = true;
    }

}

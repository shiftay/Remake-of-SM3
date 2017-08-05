using UnityEngine;
using System.Collections;

public class Bones : MonoBehaviour
{
    Animator anim;
    PlayerHealth damage;
    float direction = -1f;
    public Transform weakness;

    bool IsAlive = true;
    bool canBeDamaged = true;
    bool Death = false;

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
        
        anim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        if (gameObject.tag == "deadEnemy")
        {
            transform.position = transform.position;
            anim.SetBool("Reset", true);
            StartCoroutine(Dead());
        }

        if (IsAlive)
        {




            if (!canBeDamaged)
            {
                StartCoroutine(recentDamage());
            }
            if (direction > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                transform.Translate(new Vector3(0.5f * direction * Time.deltaTime, 0f, 0f));
            }
            else if (direction < 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                transform.Translate(new Vector3(0.5f * direction * Time.deltaTime, 0f, 0f));
            }
        }
        }


    void OnCollisionEnter2D(Collision2D other)
    {
        float height = other.contacts[0].point.y - weakness.position.y;
        if (other.gameObject.tag == "Player" && canBeDamaged)
        {
            IsAlive = false;

            anim.SetBool("Death", true);
            Kill();
           
            
            //if (other.gameObject.tag == "deadEnemy") 
            //{
            //    anim.SetBool("Death", true);

            //}

            //if (other.gameObject.tag != "deadEnemy")
            //{
            //    anim.SetBool( "Death", false);
            //}
            
        }

        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
        {
            direction *= -1;
        }

        


    }

    void Kill()
    {

        gameObject.tag = "deadEnemy";
        
    }
    IEnumerator Dead()
    {
   
    yield return new WaitForSeconds(3f);
        IsAlive = true;
        anim.SetBool("Reset", false);
        anim.SetBool("Death", false);
        gameObject.tag = "Enemy";
    }
    

    IEnumerator recentDamage()
    {
        Death = true;
        canBeDamaged = false;
        yield return new WaitForSeconds(0.5f);
        canBeDamaged = true;
    }
}
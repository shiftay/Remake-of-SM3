using UnityEngine;
using System.Collections;

public class WallCrush : MonoBehaviour
{
    Animator anim;
    PlayerHealth damage;
    float direction = -1f;
    public Transform weakness;

    bool canMove = true;
    bool canBeDamaged = true;
    #region Mutators   
    

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
       if (canMove)
            transform.Translate(new Vector3(0f, 0.35f * direction * Time.deltaTime));


        if (transform.position.y <= 0.06 && canMove)
        {
            StartCoroutine(CanMove());
        }

        if (transform.position.y >= 1.26 && canMove)
        {
            StartCoroutine(CanMove());
        }
    }


    IEnumerator CanMove()
    {
        direction *= -1;
        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
    }

    
   

   
}
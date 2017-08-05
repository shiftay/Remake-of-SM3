using UnityEngine;
using System.Collections;

public class Squish : MonoBehaviour {

    
    Animator anim;

    void Awake()
    {
        anim = GameObject.Find("Goomba").GetComponent<Animator>();

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            transform.parent.tag = "deadEnemy";
            other.rigidbody.AddForce(new Vector2(0f, 200f));
            anim.SetBool("isSquished", true);
            
            StartCoroutine(death());
        }
     }

    IEnumerator death()
    {
        yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
    }

}

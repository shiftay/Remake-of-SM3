using UnityEngine;
using System.Collections;

public class PlantBehave : MonoBehaviour 
{  
    float direction = -1f;
    float startingPos;

    float turnAround = 0.15f;
    PlayerHealth damage;

    bool allowedtoMove = true;


    // Use this for initialization
    void Start () 
    {

        startingPos = transform.position.y;
    }
	
    // Update is called once per frame
    void Update () 
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        transform.Translate(new Vector3(0f, 0.25f * Time.deltaTime * direction, 0f));

        if (transform.position.y >= startingPos)
        {
            direction *= -1f;
        }

        if (transform.position.y <= startingPos - (turnAround*1.5f))
        {
            direction *= -1f;
        }
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {                
              damage.isDamaged();
        }
    }
}



using UnityEngine;
using System.Collections;

public class boomerrangMove : MonoBehaviour {

    PlayerHealth playHealth;
    bool dmgPlayer = true;
    Vector3 startPos;
    [SerializeField]
    Vector2 centerOfMotion;
    Vector2 offset = new Vector2(.5f, 0f);
    float radiusL = 0.5f;
    float radiusH = 0.15f;
    [SerializeField]
    float x; 
    float y;
    int randomAngle;
    public Vector2 ogPos;

    public Vector3 StartPos
    {
        get { return startPos; }
        set { startPos = value; }
    }



	// Use this for initialization
	void Start () {
        playHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        ogPos = transform.position;
        centerOfMotion = (Vector2)startPos + -offset;    
    }
	
	// Update is called once per frame
	void Update () {
        randomAngle += 5;
        x = centerOfMotion.x + (radiusL * Mathf.Cos(randomAngle * Time.deltaTime));
        y = centerOfMotion.y + (radiusH * Mathf.Sin(randomAngle * Time.deltaTime));
        // Lerp to clean up animation maybe?
        transform.position = new Vector3(x, y, 0f);

        StartCoroutine(deleteBoomerrang());
	}


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && dmgPlayer)
        {
            if (playHealth.isStar)
            {
                Destroy(gameObject);
            }
            else
            {
                playHealth.isDamaged();
                StartCoroutine(canDamage());
            }
        }

        if (other.gameObject.tag == "MainCamera")
            Destroy(gameObject);

    }


    IEnumerator deleteBoomerrang()
    {

        yield return new WaitForSeconds(1.05f);
        Destroy(gameObject);
    }

    IEnumerator canDamage()
    {
        dmgPlayer = false;
        yield return new WaitForSeconds(0.25f);
        dmgPlayer = true;
    }
}

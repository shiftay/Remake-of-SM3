using UnityEngine;
using System.Collections;

public class Plant_FireBehave : MonoBehaviour
{
    public float ourPosition = 0;
    float direction = 0.5f;
    float limit = -0.11f;
    float startPos;
    float toplimit = 0.203f;
    public GameObject Ammu;
    Animation anim;
    GameObject player;
    Vector2 offset = new Vector2(0f, .01f);
    Vector2 velocity;
    bool isShooting = false;
    bool isStop = false;
     

    // Use this for initialization
    void Start()
    {
        startPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x >= ourPosition)
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
    
            if (!isStop)
            {
        //        Debug.Log("RIGHT inside first if");
                transform.Translate(new Vector3(0f, Time.deltaTime * direction, 0f));
                if (transform.position.y >= startPos + (toplimit*1.5))
                {
                    direction *= -1f;
                    StartCoroutine(IfRight());
                    isShooting = false;

                }
                if (transform.position.y <= startPos)
                {
                    direction *= -1f;
                }
            }
        }
        else
        {
            if (!isStop)
            {

                transform.Translate(new Vector3(0f, 0.25f * Time.deltaTime * direction, 0f));
                if (transform.position.y >= startPos + toplimit*1.75)
                {

                    direction *= -1f;
                    StartCoroutine(IfLeft());
                    isShooting = false;

                }
                if (transform.position.y <= startPos)
                {
                    direction *= -1f;
                }
            }
        }
            
    }
    
    
    #region Left
    IEnumerator IfLeft()
    {
        yield return new WaitForSeconds(1f);
        if (transform.position.y >= startPos + toplimit)
        {
            isStop = true;      
                if (player.transform.position.y <= startPos)
            {
                if (!isShooting)
                {
                    isShooting = true;
                    velocity = new Vector2(-1f, -1f);
                    GameObject go = (GameObject)Instantiate(Ammu, (Vector2)transform.position + offset, Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y);
                    Debug.Log("Hello");
                }
            }
            if (player.transform.position.y >= startPos)
            {
                if (!isShooting)
                {
                    isShooting = true;
                    velocity = new Vector2(-1f, 1f);
                    GameObject go = (GameObject)Instantiate(Ammu, (Vector2)transform.position + offset, Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y);
                    Debug.Log("Hello");
                }
            }
            yield return new WaitForSeconds(2f);
            isStop = false;
        }


    }
    #endregion
    #region Right
    IEnumerator IfRight()
    {
        yield return new WaitForSeconds(1f);
        if (gameObject.transform.position.y >= startPos + (toplimit * 1.5))
        {
            Debug.Log("IFRIGHT first if");
            isStop = true;
            if (player.transform.position.y <= startPos)
            {
                if (!isShooting)
                {
                    isShooting = true;
                    velocity = new Vector2(1f, -1f);
                    GameObject go = (GameObject)Instantiate(Ammu, (Vector2)transform.position + offset, Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y);
                    Debug.Log("Hello");
                }
            }
            if (player.transform.position.y >= startPos)
            {
                if (!isShooting)
                {
                    isShooting = true;
                    velocity = new Vector2(1f, 1f);
                    GameObject go = (GameObject)Instantiate(Ammu, (Vector2)transform.position + offset, Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y);
                    Debug.Log("Hello");
                }
            }
            yield return new WaitForSeconds(2f);
            isStop = false;
        }


    }
    #endregion
} 

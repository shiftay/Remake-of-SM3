using UnityEngine;
using System.Collections;

public class worldMapMove : MonoBehaviour {

    public float h, v;
    private worldMapManager wm;
    bool canMove = true;
    Vector3 lastKnown;


	// Use this for initialization
	void Start () {
        wm = GameObject.Find("WorldMapManager").GetComponent<worldMapManager>();
	}
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");


        if (h != 0 && canMove)
        {
            if (h > 0)
                transform.Translate(new Vector3(0.325f, 0f, 0f));
            else
                transform.Translate(new Vector3(-0.325f, 0f, 0f));

            StartCoroutine(moveDelay());
        }
        else if(v != 0 && canMove)
        {
            if (v > 0)
                transform.Translate(new Vector3(0f, 0.325f, 0f));
            else
                transform.Translate(new Vector3(0f, -0.325f, 0f));

            StartCoroutine(moveDelay());
        }


        //if(Input.GetButtonDown("Jump"))
        //{
            
        //    wm.isTrigger(transform.position);
        //}



	}

    



    IEnumerator moveDelay()
    {
        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
    }

}

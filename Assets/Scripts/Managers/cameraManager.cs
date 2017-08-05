using UnityEngine;
using System.Collections;

public class cameraManager : MonoBehaviour {

    GameObject character;
    Vector3 offset = new Vector3(0f, 0f,-10f);
    bool autoScroller = false;
    [SerializeField]
    float startPos = 0f;
    [SerializeField]
    float endPos = 21.4f;
    Vector3 cameraStart;
    bool world5 = false;


    public bool World5
    {
        get { return world5; }
        set { world5 = value; }
    }

    public float StartPos
    {
        get { return startPos; }
        set { startPos = value; }
    }

    public bool AutoScroller
    {
        get { return autoScroller; }
        set { autoScroller = value; }
    }

    public float EndPos
    {
        get { return endPos; }
        set { endPos = value; }
    }


    // Use this for initialization
	void Start () {
   //     character = GameObject.FindGameObjectWithTag("Player");
        cameraStart.y = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
               
        character = GameObject.FindGameObjectWithTag("Player");


        if (!autoScroller)
        {
            if (character.transform.position.x < startPos || character.transform.position.x > endPos)
                transform.position = new Vector3(transform.position.x, -0.34f, -10f);
            else
                transform.position = new Vector3(character.transform.position.x, -0.34f, -10f);

            if (character.transform.position.y > 0.2 && character.GetComponent<PlayerInputControl>().Flying)
                character.GetComponent<PlayerInputControl>().FlightCamera = true;
            else
                character.GetComponent<PlayerInputControl>().FlightCamera = false;

            if (character.GetComponent<PlayerInputControl>().FlightCamera == true && character.transform.position.y > -0.2)
                transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -10f);

            if (character.transform.position.y > 1.36f)
                transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -10f);
            if(character.transform.position.y > 2.15f)
                transform.position = new Vector3(character.transform.position.x, 2.15f, -10f);

            if (world5)
            {
                if (character.transform.position.y < -0.34f)
                    transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -10f);
                if (character.transform.position.y < -2.31f)
                    transform.position = new Vector3(character.transform.position.x, -2.5f, -10f);
            }

        }


        if (autoScroller)
        {

            if (transform.position.x > endPos)
                transform.position = transform.position;
            else
                transform.Translate(new Vector3(0.25f * Time.deltaTime, 0f, 0f));

        }



    }
}

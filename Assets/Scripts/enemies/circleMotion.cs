using UnityEngine;
using System.Collections;

public class circleMotion : MonoBehaviour {

    public Vector2 centerPoint;
    float radius = 0.5f;
    int randomAngle;

    float x, y;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        randomAngle += 5;
        x = centerPoint.x + (radius * Mathf.Cos(randomAngle * Time.deltaTime));
        y = centerPoint.y + (radius * Mathf.Sin(randomAngle * Time.deltaTime));
        // Lerp to clean up animation maybe?
        transform.position = Vector3.Lerp(new Vector3(x, y, 0f), transform.position, 0f);
        //transform.position = new Vector3(x, y, 0f);
    }
}

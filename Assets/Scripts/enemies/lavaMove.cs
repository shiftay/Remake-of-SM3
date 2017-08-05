using UnityEngine;
using System.Collections;

public class lavaMove : MonoBehaviour {

    float startPos;
    int direction = 1;
    Vector2 hldr = new Vector2(-1f, -1f);

	// Use this for initialization
	void Start () {
        startPos = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(0f, 2f * direction * Time.deltaTime, 0f);

        if (transform.position.y >= startPos + 1f)
        {
            direction *= -1;
            transform.localScale = new Vector3(1f, -1f, 1f);
        }

        if (transform.position.y <= startPos)
            Destroy(gameObject);
	}
}

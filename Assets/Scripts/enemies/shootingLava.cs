using UnityEngine;
using System.Collections;

public class shootingLava : MonoBehaviour {

    public GameObject lavaBall;
    bool canShoot = true;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (canShoot)
        {
            GameObject shot = (GameObject)Instantiate(lavaBall, transform.position, Quaternion.identity);
            StartCoroutine(briefDelay());
        }
	}

    IEnumerator briefDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }

}

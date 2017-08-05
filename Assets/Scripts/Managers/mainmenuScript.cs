using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class mainmenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void newGame()
    {
        SceneManager.LoadScene("World Map");
    }

    public void credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void matchGame()
    {
        SceneManager.LoadScene("Matching_Game");
    }

}

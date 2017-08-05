using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class endingScript : MonoBehaviour {

    int randomNum;
    Animator anim;
    GameManager gm;
    worldMapManager wm;
    int cardValue;
    int sum = 0;

    float timeRemaining;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update () {
	
	}


    void OnCollisionEnter2D(Collision2D other)
    {
        randomNum = Random.Range(0, 2);


        if (other.gameObject.tag == "Player")
        {
            other.rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            gm.EndTimer = true;
      //      timeRemaining = 



            gm.Score += (int)gm.Timer * 50;
            gm.StartTimer = false;


            // play animation and set card value for UI
            switch(randomNum)
            {
                case 0:
                    // mushroom
                    cardValue = 1;
                    gm.RandomCard = 1;
                    break;
                case 1:
                    // flower
                    cardValue = 3;
                    gm.RandomCard = 2;
                    break;
                case 2:
                    // star
                    cardValue = 4;
                    gm.RandomCard = 3;
                    break;
            }
            gm.setNewCard(cardValue);
            // finishes level at hand.
            gm.LevelCompleted = true;
            // if all 3 cards
            if(gm.Full)
            {
                for (int i = 0; i < gm.cardHolder.Length; i++)
                    sum += gm.cardHolder[i];
                // values for lives after roulette
                switch(sum)
                {
                    case 12:
                        gm.PlayerLives += 5;
                        break;
                    case 9:
                        gm.PlayerLives += 3;
                        break;
                    case 3:
                        gm.PlayerLives += 2;
                        break;
                    default:
                        gm.PlayerLives += 1;
                        break;
                }
                // clear the cards?
                gm.NeedsEmptying = true;
            }
            // calls world map on delay
            Destroy(gameObject, 0.25f);
            goToWorldMap();
        }
     }
    // sends the scene back to world map
    void goToWorldMap()
    {
        SceneManager.LoadScene("World Map");
    }


}

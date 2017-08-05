using UnityEngine;
using System.Collections;

public class selectTile : MonoBehaviour {
    Match_Player player;
    int result;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Match_Player>();
	}
	
	// Update is called once per frame
	void Update () { 


	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(Input.GetButtonDown("Jump"))
            {
                int.TryParse(gameObject.tag, out result);

                Debug.Log(result);
                player.tileSelected(result);
                
                for(int i = 0; i < player.cards.Length; i++)
                {

                    if (player.cards[i].name == gameObject.name)
                    {
                        player.cards[i].GetComponent<SpriteRenderer>().enabled = true;
                        player.positionsOfturnedCards(i);
                    }
                }
                //player.spotSelected
                
            }
        }
    }

}

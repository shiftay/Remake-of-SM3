using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Match_Player : MonoBehaviour
{
    float rightCoordinates = -0.471f;
    float leftCoordinates = 0.471f;
    float upCoordinates = 0.819f;
    float downCoordinates = -0.819f;
    GameObject player;
    public GameObject[] cards;
    public GameObject[] unknowns;
    // Use this for initialization
    [SerializeField]
    int firstValue;
    int secondValue;
    public bool first = true;
    public bool second = true;
    int hldr;

    int arraySpot1;
    int arraySpot2;
    int arrayHlrd;
    bool spot1 = true;
    bool spot2 = true;

    int Attempts = 3;
    int score = 0;

    public Text attemptsText;
    public Image gameOverImg;
    public Image winImg;
    public Text scoreText;
    public int x = 1;
    public int y = 1;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        attemptsText.text = "attempts: " + Attempts.ToString();
        scoreText.text = "score: " + score.ToString();


        if (Input.GetButtonDown("Horizontal"))
        {
            float h = Input.GetAxis("Horizontal");
            if (h < 0)
            {
                //left
                player.transform.position = new Vector3(player.transform.position.x - leftCoordinates, player.transform.position.y, player.transform.position.z);
            }
            else if (h > 0)
            {
                //right
                player.transform.position = new Vector3(player.transform.position.x - rightCoordinates, player.transform.position.y, player.transform.position.z);
            }

        }
        if (Input.GetButtonDown("Vertical"))
        {
            float v = Input.GetAxis("Vertical");
            if (v < 0)
            {
                //up
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - upCoordinates, player.transform.position.z);
            }
            else if (v > 0)
            {
                //down
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - downCoordinates, player.transform.position.z);
            }
        }


        if(!first)
        {
            if (x == 1)
                firstValue = hldr;
            else if (x == 2)
                secondValue = hldr;
            x++;
            first = true;
        }


        if(!spot1)
        {
            if (y == 1)
                arraySpot1 = arrayHlrd;
            else if (y == 2)
                arraySpot2 = arrayHlrd;
            y++;
            spot1 = true;
        }


        if (x >= 3)
            checkIfMatch();

        if (score == 900)
            youWin();


    }

    void checkIfMatch()
    {
        Debug.Log("Inside check match");
        if(firstValue == secondValue)
        {
            score += 100;
            deleteCards();
        }
        else
        {
            Attempts--;
            checkGameOver();
            cards[arraySpot1].GetComponent<SpriteRenderer>().enabled = false;
            cards[arraySpot2].GetComponent<SpriteRenderer>().enabled = false;
        }
        x = 1;
        y = 1;
    }

    void checkGameOver()
    {
        if(Attempts <= 0)
        {
            gameOverImg.enabled = true;
            StartCoroutine(gameOverDelay());
        }
    }

    void youWin()
    {
        winImg.enabled = true;
        StartCoroutine(gameOverDelay());
    }

    void deleteCards()
    {
        cards[arraySpot1].SetActive(false);
        cards[arraySpot2].SetActive(false);

        unknowns[arraySpot1].SetActive(false);
        unknowns[arraySpot2].SetActive(false);
    }



    public void tileSelected(int cardValue)
    {
        if (x < 3)
        {
            Debug.Log("setting first value");
            hldr = cardValue;
            first = false;
        }
    }

    public void positionsOfturnedCards(int cardPos)
    {
        if (y < 3)
        {
            arrayHlrd = cardPos;
            spot1 = false;
        }
    }

    IEnumerator gameOverDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}



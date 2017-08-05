using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
// Holder for now, need to find a way to have this outside
    public static GameManager instance = null;
    // values for score / lives / state and coins, all serialized for
    // play testing, remove serialized when finished
    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int playerLives = 4;
    [SerializeField]
    private int playerState = 1;
    [SerializeField]
    private int coins = 0;
    [SerializeField]
    int justFinished;

    // 1up and worldmap audio
    public AudioSource aSource;
    public AudioClip a_1up;
    public AudioClip worldMapMusic;
    worldMapManager wm;

    float timer = 300f;
    bool startTimer = false;
    bool endTimer = false;
    bool turnOffpause = false;
    public GameObject pauseText;

    // UI elements

    #region Canvas Elements
    Canvas canvasHolder;
    [SerializeField]
    private Text livesText;
    private Text scoreText;
    public Text timerText;
    private Text coinsText;

    private bool needsEmptying = false;
    bool isPaused = false;

    public bool NeedsEmptying
    {
        get { return needsEmptying; }
        set { needsEmptying = value; }
    }

    public bool TurnOffPause
    {
        get { return turnOffpause; }
        set { turnOffpause = value; }
    }

    #endregion

    #region teleporting elements
    bool onPipe = false;
    bool teleport = false;

    bool leftSub1 = false;
    bool leftSub2 = false;
    bool leftSub3 = false;
    bool leftSub5 = false;
    bool leftSub5_2 = false;
    bool leftCastleSub = false;

    public bool LeftSub1
    {
        get { return leftSub1; }
        set { leftSub1 = value; }
    }

    public bool LeftSub2
    {
        get { return leftSub2; }
        set { leftSub2 = value; }
    }

    public bool LeftSub3
    {
        get { return leftSub3; }
        set { leftSub3 = value; }
    }

    public bool LeftSub5
    {
        get { return leftSub5; }
        set { leftSub5 = value; }
    }

    public bool LeftSub5_2
    {
        get { return leftSub5_2; }
        set { leftSub5_2 = value; }
    }

    public bool LeftCastleSub
    {
        get { return leftCastleSub; }
        set { leftCastleSub = value; }
    }





    public bool OnPipe
    {
        get { return onPipe; }
        set { onPipe = value; }
    }

    public bool Teleport
    {
        get { return teleport; }
        set { teleport = value; }
    }


    #endregion

    #region WorldMap bools

    bool levelCompleted = false;
    bool level1 = false;
    bool level2 = false;
    bool level3 = false;
    bool level4 = false;
    bool level5 = false;
    bool level6 = false;
    bool castle1 = false;

    #region worldmap mutators
    public bool LevelCompleted
    {
        get { return levelCompleted; }
        set { levelCompleted = value; }
    }
    public bool Level1
    {
        get { return level1; }
        set { level1 = value; }
    }
    public bool Level2
    {
        get { return level2; }
        set { level2 = value; }
    }
    public bool Level3
    {
        get { return level3; }
        set { level3 = value; }
    }
    public bool Level4
    {
        get { return level4; }
        set { level4 = value; }
    }
    public bool Level5
    {
        get { return level5; }
        set { level5 = value; }
    }
    public bool Level6
    {
        get { return level6; }
        set { level6 = value; }
    }
    public bool Castle1
    {
        get { return castle1; }
        set { castle1 = value; }
    }
    #endregion
    #endregion

    #region Score Mutators
    public float Timer
    {
        get { return timer; }
    }
    

    public bool StartTimer
    {
        get { return startTimer; }
        set { startTimer = value; }
    }

    public bool EndTimer
    {
        get { return endTimer; }
        set { endTimer = value; }
    }


    public int JustFinished
    {
        get { return justFinished; }
        set { justFinished = value; }
    }
    public int PlayerLives
    {
        get { return playerLives; }
        set { playerLives = value; }
    }
    public int PlayerState
    {
        get { return playerState; }
        set { playerState = value; }
    }
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }


    #endregion

    #region end of level cards

    bool card1 = false;
    bool card2 = false;
    bool card3 = false;

    

    Image ui_card1;
    Image ui_card2;
    Image ui_card3;
    public Image[] cards;

    public Sprite[] resultCards;
    public int[] cardHolder = new int[3];
    int randomCard;
    bool full = false;

    public bool Full
    {
        get { return full; }
        set { full = value; }
    }

    public int RandomCard
    {
        get { return randomCard; }
        set { randomCard = value; }
    }

    #endregion

   float time = 300f;

    // Use this for initialization
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "World Map")
        {
            wm = GameObject.Find("WorldMapManager").GetComponent<worldMapManager>();
        }
       
        ui_card1 = GameObject.Find("Canvas/Card1").GetComponent<Image>();
        //// Timer Settings
        //time -= Time.deltaTime;
        //timerText.text = time.ToString("f0");

        //Check if instance already exists
        if (instance == null)
                instance = this;
           else if (instance != this)
                 Destroy(gameObject);
           DontDestroyOnLoad(gameObject);
          //Call the InitGame function to initialize the first level 
        // InitGame();
    }

	// Update is called once per frame
	void Update () {
        //getting canvas elements at all times, since the canvas is destroyed and reloaded at all times
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            ui_card1 = GameObject.Find("Canvas/Card1").GetComponent<Image>();
            ui_card2 = GameObject.Find("Canvas/Card2").GetComponent<Image>();
            ui_card3 = GameObject.Find("Canvas/Card3").GetComponent<Image>();

            livesText = GameObject.Find("Canvas/livesText").GetComponent<Text>();
            scoreText = GameObject.Find("Canvas/scoreText").GetComponent<Text>();
            coinsText = GameObject.Find("Canvas/coinsText").GetComponent<Text>();
            timerText = GameObject.Find("Canvas/timerText").GetComponent<Text>();

            pauseText = GameObject.Find("Canvas/pauseScreen");

            timerText.text = timer.ToString("f0");
            livesText.text = playerLives.ToString();
            scoreText.text = score.ToString("D7");
            coinsText.text = coins.ToString("D3");
        }



        //if (playerLives <= 0)
        //    GameOver();

        DisplayCards();

        if (startTimer && !endTimer)
            timer -= Time.deltaTime;


        if (endTimer && !startTimer)
        {
            timer = 300f;
            endTimer = false;
        }
        //upon collection 100 coins give a life
        if (coins == 100)
        {
            aSource.clip = a_1up;
            aSource.Play();
            playerLives++;
            coins = 0;
        }
        // playing world map music
        if (SceneManager.GetActiveScene().name == "World Map")
        {
            if (!aSource.isPlaying)
            {
                aSource.clip = worldMapMusic;
                aSource.Play();
            }
        }else
        {
            aSource.Stop();
        }



        if(needsEmptying)
        {
            for(int i = 0; i < cardHolder.Length; i++ )
            {
                cardHolder[i] = 0;
            }

        }


        #region switch for worldmap functionality
        switch (SceneManager.GetActiveScene().name)
        {
            case "level1":
                if (levelCompleted)
                {
                    level1 = true;
                    levelCompleted = false;
                    justFinished = 1;
                    break;
                }
                break;
            case "level2":
                if (levelCompleted)
                {
                    level2 = true;
                    levelCompleted = false;
                    justFinished = 2;
                    break;
                }
                break;
            case "level3":
                if (levelCompleted)
                {
                    level3 = true;
                    levelCompleted = false;
                    justFinished = 3;
                    break;
                    
                }
                break;
            case "level4":
                if (levelCompleted)
                {
                    level4 = true;
                    levelCompleted = false;
                    justFinished = 4;
                    break;
                }
                break;
            case "level5":
                if (levelCompleted)
                {
                    level5 = true;
                    levelCompleted = false;
                    justFinished = 6;
                    break;
                }
                break;
            case "level6":
                if (levelCompleted)
                {
                    level6 = true;
                    levelCompleted = false;
                    justFinished = 7;
                    break;
                }
                break;
            case "castle":
                if (levelCompleted)
                {
                    castle1 = true;
                    levelCompleted = false;
                    justFinished = 5;
                    break;
                }
                break;
        }
        #endregion

        setspawnPointWM(justFinished);
    }




    public void setTimer(int timer )
    {
        timerText.text = timer.ToString();
    }

    private void setspawnPointWM(int recentCompleted)
    {
        wm.LastCompleted = recentCompleted;
    }

    public void setNewCard(int cardValue)
    {

        if(!card1)
        {
            Debug.Log("in side of setNewCard");
            ui_card1.sprite = resultCards[randomCard];
            cardHolder[0] = cardValue;
            card1 = true;
        }
        else if(card1 && !card2)
        {
            //set card 2
            ui_card2.sprite = resultCards[randomCard];
            cardHolder[1] = cardValue;
            card2 = true;
        }
        else if(card1 && card2 && !card3)
        {
            //set card 3
            ui_card3.sprite = resultCards[randomCard];
            cardHolder[2] = cardValue;
            card3 = true;
            full = true;
        }
    }

    void DisplayCards()
    {
        ui_card1.sprite = resultCards[cardHolder[0]];
        ui_card2.sprite = resultCards[cardHolder[1]];
        ui_card3.sprite = resultCards[cardHolder[2]];
    }

    public bool checkGameOver()
    {
        // open menu, stop time ??
        if (playerLives == 0)
        {
            GameObject.Find("Canvas/GameOver").GetComponent<Image>().enabled = true;
            StartCoroutine(gameOverDelay());
            return true;
        }
        else
            return false;
    }
    
    IEnumerator gameOverDelay()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }

}

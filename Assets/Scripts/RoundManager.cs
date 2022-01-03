using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoundState
{
    SPECTATE,
    COUNTDOWN,
    READY,
    DRAWING,
    FINISHED,
    TIMEOUT,
    GAME_FINISHED,

    NUM_STATES
}

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    GameObject StartTimer;
    [SerializeField]
    GameObject StartText;

    [SerializeField]
    GameObject cursor;

    public int secToCountdown = 3;
    public RoundState state = RoundState.COUNTDOWN;                           //  Simple state machine
    public float finalScore;

    bool isMP;                  //  Is this round played online
    bool isHost;
    NetworkManager nManager;

    int roundCount = 0;
    bool[] isRoundWon;
    bool isScoreReceived = false;

    public Text enemyScoreText;      //  To display enemy score
    float enemyScore;

    Circles circles;

    void Awake()
    {
        nManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
        circles = GetComponent<Circles>();
        isRoundWon = new bool[3];
    }

    public void StartGame()
    {
        isMP = nManager.isGameActive;
        isHost = nManager.isRoomHost;
        roundCount = 0;

        if(isMP)
        {
            StartMP();
        }
        else
        {
            StartSP();
        }
    }

    void StartMP()
    {
        StartCoroutine(Countdown());

    }

    void StartSP()
    {
        StartCoroutine(Countdown());
    }

    float secondTimer = 0.0f;
    void EverySecondCall()
    {
        secondTimer += Time.deltaTime;  //  This breaks the function if less than 1 sec passed since last call
        if (secondTimer < 1.0f)
            return;
        secondTimer -= 1.0f;

        //Actual code that needs to be executed once per second goes below this line
        if (isMP)
            nManager.SendYourScore(finalScore);
    }


    void Update()
    {
        EverySecondCall();
        finalScore = cursor.GetComponent<Cursor>().currentScore;

        if (state == RoundState.TIMEOUT && roundCount < 3 & isScoreReceived)
        {
           EndTurn();
        }
    }

    void EndTurn()
    {
        isScoreReceived = false;

        finalScore = cursor.GetComponent<Cursor>().currentScore;
        isRoundWon[roundCount] = (finalScore > enemyScore); //  Note: if scores are the same it's considered as loosing

        if (isRoundWon[roundCount])
        {
            circles.PlusWin();
        }
        else
        {
            circles.PlusLost();
        }

        roundCount++;

        if (roundCount == 3)
        {
            FinishGame();
            state = RoundState.GAME_FINISHED;
            return;
        }

        state = RoundState.COUNTDOWN;
        StartCoroutine(Countdown());

    }

    IEnumerator Countdown()
    {
        StartText.SetActive(true);
        StartTimer.SetActive(true);

        for (int i = secToCountdown; i > 0; i--)
        {
            StartTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        StartText.SetActive(false);
        StartTimer.SetActive(false);

        state = RoundState.READY;

        cursor.SetActive(true); //  Allow player to start drawing

        cursor.GetComponent<Cursor>().StartRound();
    }

    public void UpdateScore(float newScore)
    {
        enemyScore = newScore;
        isScoreReceived = true;
        enemyScoreText.text = "P2: " + newScore.ToString() + "%";
    }

    void FinishGame()
    {
        int wins = 0;
        int lost = 0;
        int totalRounds = 0;
        foreach(bool win in isRoundWon)
        {
            if (win)
                wins++;
            else
                lost++;
            totalRounds++;
        }
        if (wins > lost)
            Debug.Log("You won! You got " + wins + " of " + totalRounds + " rounds!");
        else
            Debug.Log("You Lost! You got " + wins + " of " + totalRounds + " rounds!");

    }
}

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
    bool isYourTurn;

    bool isMP;                  //  Is this round played online
    bool isHost;
    NetworkManager nManager;

    public Text enemyScore;      //  To display enemy score


    void Awake()
    {
        nManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
    }

    public void StartGame()
    {
        isMP = nManager.isGameActive;
        isHost = nManager.isRoomHost;

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
        if(isHost)
        {
            isYourTurn = true;
            StartCoroutine(Countdown());
        }
        else
        {
            isYourTurn = false;
            StartCoroutine(Countdown());

        }


    }

    void StartSP()
    {
        isYourTurn = true;
        StartCoroutine(Countdown());
    }

    void Update()
    {


        if(state == RoundState.TIMEOUT)
        {
            finalScore = cursor.GetComponent<Cursor>().currentScore;
            if(isMP)
                nManager.SendYourScore(finalScore);
            state = RoundState.COUNTDOWN;
            StartCoroutine(Countdown());

        }
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
        if(isYourTurn)
        {
            state = RoundState.READY;
        }
        else
        {
            state = RoundState.SPECTATE;
        }
        cursor.SetActive(true); //  Allow player to start drawing

        cursor.GetComponent<Cursor>().StartRound();
    }

    public void UpdateScore(float newScore)
    {
        enemyScore.text = newScore.ToString();
    }
}

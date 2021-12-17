using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoundState
{
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

    public void StartGame()
    {
        StartCoroutine(Countdown());


    }

    void Update()
    {


        if(state == RoundState.TIMEOUT)
        {
            finalScore = cursor.GetComponent<Cursor>().currentScore;
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
        cursor.SetActive(true); //  Allow player to start drawing
        state = RoundState.READY;
        cursor.GetComponent<Cursor>().StartRound();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Player[] players;   //  Contains scores & stats for all players

    int activeTurn;

    [SerializeField]
    ScenesManager sManager;

    private static GameManager _instance;   //  Singleton pattern
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }



    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //StartCoroutine(InitialLoading());   //  Load main menu scene
        sManager.LoadRound();   //  Will be called by button later!!
        StartGame();
    }

    void StartGame()
    {
        players[0] = new Player();    //  User
        players[1] = new Player();    //  AI or online opponent

        activeTurn = UnityEngine.Random.Range(0, players.Length);

        Debug.Log(activeTurn);

        players[activeTurn].StartRound();

    }


    IEnumerator InitialLoading()
    {
        //  Do some loading stuff here (if needed)


        yield return new WaitForSeconds(2);
        sManager.LoadMainMenu();

    }
}

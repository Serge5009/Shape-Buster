using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Player player;   //  Contains scores & stats for player
    Player enemy;   //  Contains scores & stats for enemy

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
        StartCoroutine(InitialLoading());   //  Load main menu scene
        //sManager.LoadRound();   //  Will be called by button later!!

        //StartCoroutine(GameStartDelay());
    }

    void StartGame()
    {

        player = new Player();    //  User
        enemy = new Player();    //  AI or online opponent

        activeTurn = UnityEngine.Random.Range(0, 1);

        Debug.Log(activeTurn);

        GameObject roundManager = new GameObject();
        roundManager = GameObject.FindWithTag("RoundManager");
        if(roundManager == null)
            Debug.Log("No round manager!");

        RoundManager rManager = new RoundManager();
        rManager = roundManager.GetComponent<RoundManager>();
        if (rManager == null)
            Debug.Log("No round manager script!");

        rManager.StartGame();

        //player[activeTurn].StartRound();

    }


    IEnumerator GameStartDelay()
    {
        //  Do some loading stuff here (if needed)


        yield return new WaitForSeconds(1);
        StartGame();

    }

    IEnumerator InitialLoading()
    {
        //  Do some loading stuff here (if needed)


        yield return new WaitForSeconds(2);
        sManager.LoadMainMenu();

    }
}

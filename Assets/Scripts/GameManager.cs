using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerProfile player;   //  Contains scores & stats for player
    PlayerProfile enemy;   //  Contains scores & stats for enemy

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
        sManager.LoadLoading();
        sManager.loadingMode = LoadingModes.LOADING;
        StartCoroutine(InitialLoading());   //  Load main menu scene

    }

    void StartGame()
    {

        player = new PlayerProfile();    //  User
        enemy = new PlayerProfile();    //  AI or online opponent

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

    public IEnumerator GameStartDelay()
    {
        //  Do some loading stuff here (if needed)


        yield return new WaitForSeconds(0.1f);
        StartGame();

    }

    IEnumerator InitialLoading()
    {
        //  Do some loading stuff here (if needed)


        yield return new WaitForSeconds(2);
        sManager.LoadMainMenu();

    }
}

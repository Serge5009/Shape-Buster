using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject cursor;

    [SerializeField]
    Player[] players;   //  Contains scores & stats for all players

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
        players[0].StartRound();
    }




    IEnumerator InitialLoading()
    {
        //  Do some loading stuff here (if needed)


        yield return new WaitForSeconds(2);
        sManager.LoadMainMenu();

    }
}

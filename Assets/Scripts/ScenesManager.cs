using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScenes
{
    LOADING,
    MAIN_MENU,
    ROUND,
    HELP = 4,
    HIGH_SCORES,
    SETTINGS,

    NUM_SCENES
}

public class ScenesManager : MonoBehaviour
{
    public GameScenes activeScene;

    [SerializeField]
    GameManager gManager;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        activeScene = GameScenes.LOADING;
    }

    public void LoadLoading()
    {
        //SceneManager.LoadScene(0);
        //activeScene = GameScenes.LOADING;
        Debug.Log("Simulating loading scene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
        activeScene = GameScenes.MAIN_MENU;
    }

    public void LoadRound()
    {
        SceneManager.LoadScene("Round UI");                  //  Load scene
        activeScene = GameScenes.ROUND;             //  Set state
        StartCoroutine(gManager.GameStartDelay());  //  Call round start after short delay
    }

    public void LoadHelp()
    {
        SceneManager.LoadScene(4);
        activeScene = GameScenes.HELP;
    }

    public void LoadHighScores()
    {
        SceneManager.LoadScene(5);
        activeScene = GameScenes.HIGH_SCORES;
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene(6);
        activeScene = GameScenes.SETTINGS;
    }
}

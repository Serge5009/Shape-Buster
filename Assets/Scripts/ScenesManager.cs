using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScenes
{
    LOADING,
    MAIN_MENU,
    ROUND,
    HELP,
    HIGH_SCORES,
    SETTINGS,

    NUM_SCENES
}

public enum LoadingModes
{
    LOADING,
    SINGLEPLAYER,
    MULTIPLAYER,

    NUM_LOADING_MODES
}

public class ScenesManager : MonoBehaviour
{
    public LoadingModes loadingMode = LoadingModes.LOADING;

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
        SceneManager.LoadScene("Loading");
        activeScene = GameScenes.LOADING;
        //Debug.Log("Simulating loading scene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        activeScene = GameScenes.MAIN_MENU;
    }

    public void LoadRound()
    {
        SceneManager.LoadScene("Round UI");         //  Load scene
        activeScene = GameScenes.ROUND;             //  Set state
        StartCoroutine(gManager.GameStartDelay());  //  Call round start after short delay
    }

    public void LoadHelp()
    {
        SceneManager.LoadScene("Help");
        activeScene = GameScenes.HELP;
    }

    public void LoadHighScores()
    {
        SceneManager.LoadScene("HighScores");
        activeScene = GameScenes.HIGH_SCORES;
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
        activeScene = GameScenes.SETTINGS;
    }
}

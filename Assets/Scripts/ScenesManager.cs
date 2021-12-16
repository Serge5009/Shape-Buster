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

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        activeScene = GameScenes.LOADING;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
        activeScene = GameScenes.MAIN_MENU;
    }

    public void LoadRound()
    {
        SceneManager.LoadScene(2);
        activeScene = GameScenes.ROUND;
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

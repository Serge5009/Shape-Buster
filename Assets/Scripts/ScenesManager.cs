using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadRound()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadHelp()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadHighScores()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene(6);
    }
}

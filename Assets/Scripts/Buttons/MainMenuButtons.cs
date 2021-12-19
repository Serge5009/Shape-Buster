using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
	[SerializeField]
	ScenesManager sManager;

	FindEnemy mpScript;

	public Button SingleplayerButton;
	public Button MultiplayerButton;
	public Button HelpButton;
	public Button QuitButton;
	public Button LeaderboardButton;
	public Button SettingsButton;



	void Awake()
    {
		sManager = GameObject.FindWithTag("SceneManager").GetComponent<ScenesManager>();
		mpScript = this.GetComponent<FindEnemy>();
	}

	void Start()
	{
		Button singleplayerBtn = SingleplayerButton.GetComponent<Button>();
		singleplayerBtn.onClick.AddListener(OnSingleplayerClick);

		Button multiplayerBtn = MultiplayerButton.GetComponent<Button>();
		multiplayerBtn.onClick.AddListener(OnMultiplayerClick);

		Button helpBtn = HelpButton.GetComponent<Button>();
		helpBtn.onClick.AddListener(OnHelpClick);

		Button quitBtn = QuitButton.GetComponent<Button>();
		quitBtn.onClick.AddListener(OnQuitClick);

		Button leaderboardBtn = LeaderboardButton.GetComponent<Button>();
		leaderboardBtn.onClick.AddListener(OnLeaderboardClick);

		Button settingsBtn = SettingsButton.GetComponent<Button>();
		settingsBtn.onClick.AddListener(OnSettingsClick);
	}

	void OnSingleplayerClick()
	{
		sManager.LoadRound();
	}

	void OnMultiplayerClick()
	{
		//sManager.LoadRound();
		mpScript.StartMP();
	}

	void OnHelpClick()
	{
		sManager.LoadHelp();
	}

	void OnQuitClick()
	{
		//	Quit
	}

	void OnLeaderboardClick()
	{
		sManager.LoadHighScores();
	}

	void OnSettingsClick()
	{
		sManager.LoadSettings();
	}
}

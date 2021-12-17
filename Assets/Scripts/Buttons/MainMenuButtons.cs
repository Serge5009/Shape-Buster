using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
	[SerializeField]
	ScenesManager sManager;

	public Button SinglePlayerButton;

	void Awake()
    {
		sManager = GameObject.FindWithTag("SceneManager").GetComponent<ScenesManager>();
	}

	void Start()
	{
		Button btn = SinglePlayerButton.GetComponent<Button>();
		btn.onClick.AddListener(OnSinglePlayerClick);
	}

	void OnSinglePlayerClick()
	{
		sManager.LoadRound();
	}
}

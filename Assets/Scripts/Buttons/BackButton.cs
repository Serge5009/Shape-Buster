using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
	[SerializeField]
	ScenesManager sManager;

	Button backButton;


	void Awake()
    {
		backButton = this.GetComponent<Button>();
		sManager = GameObject.FindWithTag("SceneManager").GetComponent<ScenesManager>();
	}

	void Start()
	{
		Button backBtn = backButton.GetComponent<Button>();
		backBtn.onClick.AddListener(OnBackClick);


	}

	void OnBackClick()
	{
		sManager.LoadMainMenu();
	}
}

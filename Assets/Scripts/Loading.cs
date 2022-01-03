using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Loading : MonoBehaviour
{
    LoadingModes mode;
    ScenesManager sManager;

    [SerializeField]
    Text modeText;
    string modeStr;

    [SerializeField] 
    Text loadingText;
    string loadingStr;

    void Start()
    {
        sManager = GameObject.FindWithTag("SceneManager").GetComponent<ScenesManager>();
        mode = sManager.loadingMode;

    }

    void Update()
    {
        if(mode == LoadingModes.LOADING)
        {
            loadingStr = "Loading";
            modeStr = "Shape Buster!";
        }
        else if (mode == LoadingModes.SINGLEPLAYER)
        {
            loadingStr = "Loading";
            modeStr = "Singleplayer";
        }
        else if (mode == LoadingModes.MULTIPLAYER)
        {
            loadingStr = "looking for opponent";
            modeStr = "Multiplayer";
        }

        AddDots();

        loadingText.text = loadingStr;
        modeText.text = modeStr;
    }


    int dotCounter = 0;
    float dotTimer = 0.0f;
    void AddDots()
    {
        dotTimer += Time.deltaTime;  //  This breaks the function if less than 1 sec passed since last call
        if (dotTimer > 0.25f)
        {
            dotTimer -= 0.25f;
            dotCounter++;

            if (dotCounter >= 4)
                dotCounter = 0;
        }

        for (int i = 0; i < dotCounter; i++)
            loadingStr += ".";
    }
}

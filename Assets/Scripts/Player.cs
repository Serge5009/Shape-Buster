using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score;   //  Score tracking

    [SerializeField]
    GameObject cursor;

    [SerializeField]
    GameObject cursorPrefab;

    void Awake()
    {
        //Instantiate(cursorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        cursor = GameObject.FindGameObjectsWithTag("Cursor")[0];
        // cursor = new Cursor();
    }

    public void StartRound()
    {

        //cursor.SetActive(true); //  Allow player to start drawing
    }
}

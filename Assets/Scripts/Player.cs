using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score;   //  Score tracking
    [SerializeField]
    GameObject cursor;  

    public void StartRound()
    {
        cursor.SetActive(true); //  Allow player to start drawing
    }
}

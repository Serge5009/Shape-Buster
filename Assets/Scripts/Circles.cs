using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circles : MonoBehaviour
{
    [SerializeField]
    GameObject[] P1Circles;
    [SerializeField]
    GameObject[] P2Circles;



    void Start()
    {
        foreach (GameObject i in P1Circles)
            i.SetActive(false);
        foreach (GameObject i in P2Circles)
            i.SetActive(false);
    }

    public void PlusWin()
    {
        int i = 0;
        while (P1Circles[i].activeSelf)
            i++;
        P1Circles[i].SetActive(true);
    }

    public void PlusLost()
    {
        int i = 0;
        while (P2Circles[i].activeSelf)
            i++;
        P2Circles[i].SetActive(true);

    }


    public void UpdateBar()
    {



    }

}

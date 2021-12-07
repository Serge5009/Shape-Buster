using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject centralPoint;             //  Screen center, needed to calculate circle quality
    GameObject canvas;                          //  Canvas, to put new dots as its children
    [SerializeField] GameObject pointPrefab;    //  Prefab for single dot (part of drawn circle)
    GameObject[] allPoints;                     //  Array of all points in game
    public int maxPoints = 500;                 //  Max number of points until failed try
    int numPoints = 0;                          //  Point counter
    bool isPainting = false;                    //  Click control
    

    Vector3 worldPosition;


    void Start()
    {
        allPoints = new GameObject[maxPoints];  //  Populate array
        canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];    //  Link canvas by Tag
    }

    void Update()
    {
        //  Find cursor position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        //  Add iamge to cursor
        this.transform.position = mousePos;

        if (Input.GetMouseButtonDown(0))    //  When LMB is pressed
        {
            isPainting = true;
        }

        if(isPainting)
        { 
            if(numPoints < maxPoints)  //  Return if point max is reached
            {
            //  Calculate distance from center to cursor
            float distance = Vector3.Distance(centralPoint.transform.position, mousePos);
            Debug.Log("Distance = " + distance);

                //  TODO: We want to track time between each point spawn and prevent 2 points from spawning too close
                //  for it we will remember the position of the last dot. It will also help to scale the new dot

                //  TODO: Add color code here

                //  TODO: Add score managing here


                //  Adding new point
                SpawnDot(mousePos);
            }
            else
            {
                Debug.Log("Ran out of dots!");
            }
        }

        //  TODO: Add network code here

    }

    void SpawnDot(Vector3 mousePos)
    {
        allPoints[numPoints] = (GameObject)Instantiate(pointPrefab, mousePos, Quaternion.identity);
        allPoints[numPoints].transform.parent = canvas.transform;

        numPoints++;    //  Counter
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject centralPoint;             //  Screen center, needed to calculate circle quality
    public GameObject canvas;                   //  Canvas, to put new dots as its children
    public int maxPoints = 5000;                //  Max number of points until failed try
    [SerializeField] GameObject pointPrefab;    //  Prefab for single dot (part of drawn circle)
    GameObject[] allPoints;                     //  Array of all points in game
    int numPoints = 0;                          //  Point counter

    Vector3 worldPosition;


    void Start()
    {
        allPoints = new GameObject[maxPoints];  //  Populate array
    }

    void Update()
    {
        //  Find cursor position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        //  Add iamge to cursor
        this.transform.position = mousePos;

        if (Input.GetMouseButton(0))    //  When LMB is pressed
        {
            if(numPoints >= maxPoints)  //  Return if point max is reached
            {
                return;
            }

            //  Calculate distance from center to cursor
            float distance = Vector3.Distance(centralPoint.transform.position, mousePos);
            Debug.Log("Distance = " + distance);

                //  TODO: Add color code here

                //  TODO: Add score managing here


            //  Adding new point
            allPoints[numPoints] = (GameObject)Instantiate(pointPrefab, mousePos, Quaternion.identity);
            allPoints[numPoints].transform.parent = canvas.transform;

            numPoints++;    //  Counter
        }

        //  TODO: Add network code here

    }

}

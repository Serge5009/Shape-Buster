using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject centralPoint;
    public GameObject canvas;
    public int maxPoints = 5000;
    [SerializeField] GameObject pointPrefab;
    Vector3 worldPosition;
    GameObject[] allPositions;
    int numPoints = 0;


    void Start()
    {
        allPositions = new GameObject[maxPoints];
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        this.transform.position = mousePos;

        if (Input.GetMouseButton(0))
        {
            if(numPoints >= maxPoints)
            {
                return;
            }

            float distance = Vector3.Distance(centralPoint.transform.position, mousePos);
            Debug.Log("Distance = " + distance);

            allPositions[numPoints] = (GameObject)Instantiate(pointPrefab, mousePos, Quaternion.identity);
            allPositions[numPoints].transform.parent = canvas.transform;

            numPoints++;
        }
    }

}

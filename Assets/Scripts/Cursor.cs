using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    public GameObject centralPoint;             //  Screen center, needed to calculate circle quality
    GameObject canvas;                          //  Canvas, to put new dots as its children
    [SerializeField] GameObject pointPrefab;    //  Prefab for single dot (part of drawn circle)
    GameObject[] allPoints;                     //  Array of all points in game
    List<float> pointScore;                     //  Array of all individual dots score
    Vector3 mousePos;


    public int maxPoints = 500;                 //  Max number of points until failed try
    int numPoints = 0;                          //  Point counter

    RoundManager rManager;                      //  Round Manager

    //TIME TRACKING
    public float timeToDraw;
    float timeLeft;
    float timeFromLastDot;
    public Text timer;

    //LAST POINT
    Vector3 lastPosition;
    float lastSize;
    public float requiredOffset = 3.0f;

    float lastDistance;
    float targetDistance;

    float farDistance;      //  Distances to be considered too far/close to the center
    float closeDistance;    //  and will give 0% score

    //COLOR
    public Color rightColor;
    public Color wrongColor;

    public Text scoreText;      //  To display score
    public float currentScore = 0;


    //Vector3 worldPosition;


    void Awake()
    {
        LoadRound();
        rManager.state = RoundState.READY;
        timeLeft = timeToDraw;
        pointScore = new List<float>();
    }


    void LoadRound()
    {

        allPoints = new GameObject[maxPoints];  //  Populate array
        canvas = GameObject.FindGameObjectsWithTag("GameCanvas")[0];    //  Link canvas by Tag

        GameObject roundManager = new GameObject();
        roundManager = GameObject.FindWithTag("RoundManager");
        if (roundManager == null)
            Debug.Log("No round manager!");

        rManager = new RoundManager();
        rManager = roundManager.GetComponent<RoundManager>();
        if (rManager == null)
            Debug.Log("No round manager script!");
    }

    public void StartRound()
    {
        timeLeft = timeToDraw;
        rManager.state = RoundState.READY;

        foreach (GameObject i in allPoints)
        {   //  Delete all old points
            Destroy(i);
        }
        numPoints = 0;  //  Reset counter

        pointScore.Clear();
    }

    void Update()
    {
        //  Find cursor position
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        //worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        //  Add iamge to cursor
        this.transform.position = mousePos;

        ClickProcess(); //  State control based on mouse clics
        TimeProcess();  //  Controls all timers
        CurrentScore(); //  Calculates score and saves it to float currentScore
        UpdateText();   //  Keeps UI text up to date

        

        //  TODO: Add network code here

    }

    void FixedUpdate()
    {
        if (rManager.state == RoundState.DRAWING)
        {
            if (numPoints < maxPoints)
            {
                //  Adding new point
                float lastDistance = Vector3.Distance(lastPosition, mousePos);
                if (lastDistance >= requiredOffset)
                {
                    SpawnDot(mousePos);
                }
            }
            else
            {
                Debug.Log("Ran out of dots!");
                StopDrawing();
            }
        }
    }

    void ClickProcess() //  Calls state-related function according to mouse input & current state
    {
        if (Input.GetMouseButtonDown(0) && rManager.state == RoundState.READY)    //  When LMB first clicked
        {
            StartDrawing();
        }
        if (Input.GetMouseButtonUp(0) && rManager.state == RoundState.DRAWING)    //  When LMB released
        {
            StopDrawing();
        }
        if (Input.GetMouseButtonDown(0) && rManager.state == RoundState.FINISHED)    //  When LMB first clicked
        {
            Retry();
        }
    }

    void TimeProcess()
    {
        timeLeft -= Time.deltaTime;
        timeFromLastDot += Time.deltaTime;
        if (rManager.state == RoundState.TIMEOUT || rManager.state == RoundState.COUNTDOWN)
            timeLeft = 0;

        //  Later: sync with countdown

        if (timeLeft <= 0)
        {
            if(rManager.state != RoundState.TIMEOUT && rManager.state != RoundState.SPECTATE && rManager.state != RoundState.COUNTDOWN)
                Finish();
        }    
    }

    void SpawnDot(Vector3 mousePos)
    {
        allPoints[numPoints] = (GameObject)Instantiate(pointPrefab, mousePos, Quaternion.identity);
        allPoints[numPoints].transform.parent = canvas.transform;
        allPoints[numPoints].transform.localScale = NewDotScale();

        lastPosition = mousePos;
        timeFromLastDot = 0;

        //  Calculate distance from center to cursor
        float distance = Vector3.Distance(centralPoint.transform.position, mousePos);

        if(numPoints == 0)//    If it's the first dot spawned
        {
            targetDistance = distance;
            farDistance = distance * 2;
            closeDistance = distance / 2;
        }

        NewDotScore(distance);
        NewDotPaint();

        numPoints++;    //  Counter
    }

    void StartDrawing()
    {
        rManager.state = RoundState.DRAWING;
    }

    void Finish()
    {

        rManager.state = RoundState.TIMEOUT;

    }

    void StopDrawing()
    {
        rManager.state = RoundState.FINISHED;
    }

    void Retry()    //  Restart the level for another try
    {
        foreach(GameObject i in allPoints)
        {   //  Delete all old points
            Destroy(i);
        }
        numPoints = 0;  //  Reset counter
        pointScore.Clear();
        StartDrawing(); //  Start next try
    }

    Vector3 NewDotScale()
    {
        Vector3 scale;
        float scaleChange = Mathf.Pow(1.05f, timeFromLastDot * 100) - 1.1f;
        float scaleFactor;
        scaleFactor = lastSize + scaleChange;
        //scaleFactor *= 0.8f;

        if (scaleFactor > 3.0f)
            scaleFactor = 3.0f;

        if (scaleFactor < 0.7f)
            scaleFactor = 0.7f;

        scale.x = scaleFactor;
        scale.y = scaleFactor;
        scale.z = 1.0f;

        lastSize = scaleFactor;
        return scale;
    }

    void NewDotScore(float dist)
    {
        float score = 100.0f; //  Maximal point score

        if(dist > targetDistance)
        {
            score = 1.0f - (dist - targetDistance) / (farDistance - targetDistance);
            score *= 100.0f;
        }
        if (dist < targetDistance)
        {
            score = (dist - closeDistance) / (targetDistance - closeDistance);
            score *= 100.0f;
        }
        else
        {
            //  Do nothing, this dot is perfect
        }

        if (score < 0)  //  To prevent negative score
            score = 0;

        //Debug.Log("Last score: " + score);
        pointScore.Add(score);

    }

    void NewDotPaint()
    {
        float colorFactor = pointScore[numPoints] / 100.0f;

        Color dotColor = Color.Lerp(wrongColor, rightColor, colorFactor);

        allPoints[numPoints].GetComponent<Image>().color = dotColor;

    }

    void CurrentScore()
    {
        if(rManager.state == RoundState.READY)  // Before we start drawing
        {
            currentScore = 0;                   // Score will be just 0
            return;
        }

        float scoreSum = 0;
        foreach (float i in pointScore)
        {
            scoreSum += i;
        }
        scoreSum /= pointScore.Count;
        currentScore = Mathf.Round(scoreSum);
    }

    void UpdateText()   //  Called every update, refresh UI text
    {

        scoreText.text = ("P1: " + currentScore.ToString() + "%");
        timer.text = timeLeft.ToString();


    }

    void Reset()
    {
        Retry();
        timeLeft = timeToDraw;
    }

}
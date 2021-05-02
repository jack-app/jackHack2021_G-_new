using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmusubiGenerator : MonoBehaviour
{
    private Vector2 startLocation,currentLocation,nearestOmusubi;
    private bool started = false;
    public int numberOfOmusubi;
    public List<Vector2> omusubiLocationList;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void CallStartGame()
    {
        if (started == false)
        {
            StartCoroutine(StartGame());
            started = true;
        }
    }
    private IEnumerator StartGame()
    {
        var locationNow = new Utilities().GPSCoroutine();
        yield return StartCoroutine(locationNow);
        startLocation = (Vector2)locationNow.Current;
        SetOmusubi();
    }
    void SetOmusubi()
    {
        for(int i = 0; i < numberOfOmusubi; i++)
        {
            omusubiLocationList.Add(new Vector2(startLocation.x + Random.Range(0.1f,1.0f), startLocation.y + Random.Range(0.1f, 1.0f)));
        }
        foreach (Vector2 omusubiLocation in omusubiLocationList)
        {
            Debug.Log(omusubiLocation.ToString());
        }
    }
    public void CallDetectAngle()
    {
        if (started == true)
        {
            StartCoroutine(DetectAngle());
        }
    }
    private IEnumerator DetectAngle()
    {
        var locationNow = new Utilities().GPSCoroutine();
        yield return StartCoroutine(locationNow);
        currentLocation = (Vector2)locationNow.Current;
        CalculateAngle();
    }
    void CalculateAngle()
    {
        float minDist = Mathf.Infinity;
        foreach(Vector2 omusubiLocation in omusubiLocationList)
        {
            float distance = Vector2.Distance(omusubiLocation, currentLocation);
            if(distance < minDist)
            {
                nearestOmusubi = omusubiLocation;
                minDist = distance;
            }
        }
        angle = Vector2.Angle(nearestOmusubi, currentLocation);
        Debug.Log(angle);
    }
}

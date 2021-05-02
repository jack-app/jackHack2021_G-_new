using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OmusubiAngleDetector : MonoBehaviour
{
    TextMeshProUGUI northText,southText,eastText,westText;
    public Vector2 currentLocation, nearestOmusubi;
    OmusubiGenerator OG;
    public float angle,distance;
    // Start is called before the first frame update
    void Start()
    {
        OG = GetComponent<OmusubiGenerator>();
        northText = GameObject.Find("North").GetComponent<TextMeshProUGUI>();
        southText = GameObject.Find("South").GetComponent<TextMeshProUGUI>();
        eastText = GameObject.Find("East").GetComponent<TextMeshProUGUI>();
        westText = GameObject.Find("West").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CallDetectAngle()
    {
        if (OG.started == true)
        {
            StartCoroutine(DetectAngle());
        }
    }
    private IEnumerator DetectAngle()
    {
        var locationNow = new Utilities().GPSCoroutine();
        yield return StartCoroutine(locationNow);
        currentLocation = (Vector2)locationNow.Current;
        ShowAngle();
    }
    void ShowAngle()
    {
        float minDist = Mathf.Infinity;
        foreach (Vector2 omusubiLocation in OG.omusubiLocationList)
        {
            distance = Utilities.GetDistanceFromGPS(currentLocation, omusubiLocation);
            if (distance < minDist)
            {
                nearestOmusubi = omusubiLocation;
                minDist = distance;
            }
        }
        //Œo“x‚Ì·•ª
        float xDelta = currentLocation.x - nearestOmusubi.x;
        //ˆÜ“x‚Ì·•ª
        float yDelta = currentLocation.y - nearestOmusubi.y;
        //0.005‚ÌŽU‚ç‚Î‚è
        //137.1749 35.05275
        if(xDelta >= 0)
        {
            westText.fontSize = Mathf.Min(10 * 0.005f/xDelta,150);
            eastText.fontSize = 0;
        }
        else
        {
            eastText.fontSize = Mathf.Min(10 * -0.005f/xDelta,150);
            westText.fontSize = 0;
        }
        if(yDelta >= 0)
        {
            northText.fontSize = Mathf.Min(10 * 0.005f / yDelta,150);
            southText.fontSize = 0;
        }
        else
        {
            southText.fontSize = Mathf.Min(10 * -0.005f / yDelta,150);
            northText.fontSize = 0;
        }
    }
}

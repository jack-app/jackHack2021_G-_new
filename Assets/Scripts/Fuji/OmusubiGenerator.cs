using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmusubiGenerator : MonoBehaviour
{
    private Vector2 startLocation;
    public bool started = false;
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
            omusubiLocationList.Add(new Vector2(startLocation.x + Random.Range(-0.005f,0.005f), startLocation.y + Random.Range(-0.005f, 0.005f)));
        }
    }
}

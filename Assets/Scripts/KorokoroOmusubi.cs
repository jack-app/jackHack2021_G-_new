using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KorokoroOmusubi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCorocoro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartCorocoro()
    {
        var gpsCoroutine = new Utilities().GPSCoroutine();
        yield return StartCoroutine(gpsCoroutine);
        Vector2 gps = (Vector2)gpsCoroutine.Current;
        var gps2 =gps+ Utilities.MeterToGPS(new Vector2(1000,1000), gps.y);


        while (true)
        {


            yield return new WaitForSeconds(1);
        }


    }

}

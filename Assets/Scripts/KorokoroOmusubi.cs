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
        var utilities = Utilities.Instance;

        var gpsCoroutine = utilities.GPSCoroutine();
        yield return StartCoroutine(gpsCoroutine);
        Vector2 gps = (Vector2)gpsCoroutine.Current;
        var gps2 =gps+ Utilities.MeterToGPS(new Vector2(0,1000), gps.y);


        while (true)
        {
            var ComparingConnpassCoroutine = utilities.ComparingConnpass(gps2);


            yield return StartCoroutine(ComparingConnpassCoroutine);




            print((float)ComparingConnpassCoroutine.Current);



            
            yield return StartCoroutine(gpsCoroutine);
            gps = (Vector2)gpsCoroutine.Current;

            print(Utilities.GetDistanceFromGPS(gps, gps2));
        }


    }

}


public class Destination
{
    Vector2 startGPS;


}

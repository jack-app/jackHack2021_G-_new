using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    public static Location Instance { set; get; }

    public float latitude;
    public float longitude;
    public float altitude;

    private void Start()
    {
        print(Utilities.MeterPerLongitudeX(70));

        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
        StartCoroutine(StartDirectionService());
    }


    private void Update()
    {
        //print(Input.location.isEnabledByUser);
    }

    public IEnumerator enumerator;
    public IEnumerator StartLocationService()
    {
        var coroutine = new Utilities().GPSCoroutine();
        yield return StartCoroutine(coroutine);

        Vector2 gps = (Vector2)coroutine.Current;
        longitude = gps.x;
        latitude = gps.y;
        print(gps.x);
    }

    public GameObject japan;
    public IEnumerator StartDirectionService()
    {
        
        while (true)
        {
            var coroutine = new Utilities().DirectionCoroutine();
            yield return StartCoroutine(coroutine);
            //float heading= (float)coroutine.Current;
            //japan.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, heading);

            


            yield return null;
        }
    }
}
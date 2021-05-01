using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public static Location Instance { set; get; }

    public float latitude;
    public float longitude;
    public float altitude;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
    }


    private void Update()
    {
        //print(Input.location.isEnabledByUser);
    }
    private IEnumerator StartLocationService()
    {
        // First, check if user has location service enabled
        while(!Input.location.isEnabledByUser)
        {
            Debug.Log("GPS not enabled");
            yield return null;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait <= 0)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }

        while(Input.location.status != LocationServiceStatus.Running)
        {
            yield return null;
        }

        print(Input.location.status);

        // Set locational infomations
        while (true)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;
            yield return new WaitForSeconds(10);
        }
    }
}
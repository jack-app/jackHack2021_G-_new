using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchMode_GPS : MonoBehaviour
{
    public LocationInfo location;
    public LocationServiceStatus status;
    public float interval,x,y;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        this.status = Input.location.status;
        if (Input.location.isEnabledByUser)
        {
            switch (this.status)
            {
                case LocationServiceStatus.Stopped:
                    Input.location.Start();
                    break;

                case LocationServiceStatus.Running:
                    this.location = Input.location.lastData;
                    x = location.longitude;
                    y = location.latitude;
                    break;

                default:
                    break;
            }
        }
        else
        {
            Debug.Log("location is disabled");
        }
        yield return new WaitForSeconds(interval);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.status + "," + x.ToString() + ","+ y.ToString());
    }
}

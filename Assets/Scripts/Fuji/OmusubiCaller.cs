using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmusubiCaller : MonoBehaviour
{
    OmusubiGenerator OG;
    OmusubiAngleDetector OAD;
    // Start is called before the first frame update
    void Start()
    {
        OG = GetComponent<OmusubiGenerator>();
        OAD = GetComponent<OmusubiAngleDetector>();
    }

    // Update is called once per frame
    public void CallOmusubi()
    {
        if(OAD.distance <= 30)
        {
            Debug.Log("Found Omusubi!");
            //リストの何番か特定するの難しいんで、最も近いおむすびを北極へ
            OAD.nearestOmusubi = new Vector2(0, 0);
        }
        else
        {
            Debug.Log("No Omusubi here...");
        }
    }
}

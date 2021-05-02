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
        if(OAD.currentDistance <= 30)
        {
            Debug.Log("Found Omusubi!");
            OG.omusubiLocationList.RemoveAt(OG.omusubiLocationList.IndexOf(OAD.nearestOmusubi));
            OAD.CallDetectAngle();
        }
        else
        {
            Debug.Log("No Omusubi around here...");
        }
    }
}

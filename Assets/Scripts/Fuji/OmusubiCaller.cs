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
            //ƒŠƒXƒg‚Ì‰½”Ô‚©“Á’è‚·‚é‚Ì“ï‚µ‚¢‚ñ‚ÅAÅ‚à‹ß‚¢‚¨‚Þ‚·‚Ñ‚ð–k‹É‚Ö
            OAD.nearestOmusubi = new Vector2(0, 0);
        }
        else
        {
            Debug.Log("No Omusubi here...");
        }
    }
}

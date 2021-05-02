using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmusubiCaller : MonoBehaviour
{
    OmusubiGenerator OG;
    OmusubiAngleDetector OAD;
    OmusubiKunAnimation OA;
    // Start is called before the first frame update
    void Start()
    {
        OG = GetComponent<OmusubiGenerator>();
        OAD = GetComponent<OmusubiAngleDetector>();
        OA = GameObject.Find("omusubi_").GetComponent<OmusubiKunAnimation>();
    }

    // Update is called once per frame
    public void CallOmusubi()
    {
        //最寄りおむすびの距離が30m未満なら見つけて、最寄りおむすびをリストから消す。
        if(OAD.currentDistance <= 50)
        {
            OA.Found();
            StartCoroutine(WaitAndDeleteList());
        }
        else
        {
            Debug.Log("No Omusubi around here...");
        }
    }
    IEnumerator WaitAndDeleteList()
    {
        yield return new WaitForSeconds(2.458f);
        OG.omusubiLocationList.RemoveAt(OG.omusubiLocationList.IndexOf(OAD.nearestOmusubi));
        OAD.CallDetectAngle();
    }
}

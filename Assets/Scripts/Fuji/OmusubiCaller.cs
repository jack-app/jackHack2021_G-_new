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
        //�Ŋ�肨�ނ��т̋�����30m�����Ȃ猩���āA�Ŋ�肨�ނ��т����X�g��������B
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

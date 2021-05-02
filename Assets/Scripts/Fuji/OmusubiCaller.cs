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
            //���X�g�̉��Ԃ����肷��̓����ŁA�ł��߂����ނ��т�k�ɂ�
            OAD.nearestOmusubi = new Vector2(0, 0);
        }
        else
        {
            Debug.Log("No Omusubi here...");
        }
    }
}

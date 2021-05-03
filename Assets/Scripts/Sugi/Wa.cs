using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wa : MonoBehaviour
{
    public GameObject voice;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Voice", 1, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void Voice()
    {
        Instantiate(voice, transform.position + Vector3.up*4, Quaternion.identity);
    }

    public void Cancel()
    {
        CancelInvoke("Voice");
    }
}

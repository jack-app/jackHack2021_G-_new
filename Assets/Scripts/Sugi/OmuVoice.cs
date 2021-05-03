using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmuVoice : MonoBehaviour
{

    public GameObject voice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Voice()
    {
        Instantiate(voice, transform.position + Vector3.up, Quaternion.identity);
    }


    public void VoicePlay()
    {
        InvokeRepeating("Voice", 10, 3);
    }
}

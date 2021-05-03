using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{

    Vector3 size;
    // Start is called before the first frame update
    void Start()
    {
        size = transform.localScale;
    }

    int a = 0;
    // Update is called once per frame
    void Update()
    {
        a++;
        transform.localScale = size * (Mathf.Cos(a * 0.03f) * 0.05f + 1f);

    }
}

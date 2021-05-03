using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    RectTransform rectTransform;
    Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        scale = rectTransform.localScale;
    }

    int a = 0;
    bool peek = false;
    // Update is called once per frame
    void Update()
    {

        float b = Mathf.Min(1.0f, a * 0.01f);
        if (b == 1.0f&!peek) { peek = true; a = 0; }
        if (peek) { b = 1 + Mathf.Sin(a * 0.02f)/2*Mathf.Max(0.1f,1-a*0.005f) ; }
        a++;
        rectTransform.localScale = b*scale;
    }
}

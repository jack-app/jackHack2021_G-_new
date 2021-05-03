using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRot : MonoBehaviour
{

    RectTransform rectTransform;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        pos = rectTransform.position;
    }

    int a = 0;
    // Update is called once per frame
    void Update()
    {
        rectTransform.Rotate(0, 0, 2);

        rectTransform.position = new Vector3(pos.x+350-a*1.5f,pos.y+10*Mathf.Sin(a*0.01f),0);
        a++;


        if (a * 1.5f > 700)
        {
            a = 0;
        }
    }
}

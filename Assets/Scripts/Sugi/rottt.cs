using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rottt : MonoBehaviour
{

    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.Rotate(0, 0, 0.2f);
    }
}

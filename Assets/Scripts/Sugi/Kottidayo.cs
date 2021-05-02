using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kottidayo : MonoBehaviour
{
    GameObject mainCamera;
    Vector3 startPos;
    int a = 0;
    int b = 0;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        startPos = transform.position;
        b = (int)(Random.value * 10);
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.position - transform.position);


    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.position - transform.position);
        a++;
        transform.position = startPos + Vector3.up * 0.01f * a + transform.right * Mathf.Sin(b+a*0.01f);

        float alpha = 1 - 0.002f * a;

        if (alpha < 0) { Destroy(this.gameObject); }
        spriteRenderer.color =new Color(1,1,1,alpha);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hiroScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int x;
        x = 1;
        x = x + 2;
        Debug.Log(x);

        int sum = 0;
        for(int i=1;i<=100;i++)
        {
            sum += i;
        }
        Debug.Log(sum);

        sum = 0;
        int fibo = 0;
        int fibo2 = 1;
        int fibo3 = 0;
        for(int i=0;i<10;i++)
        {
            fibo3 = fibo + fibo2;
            Debug.Log(fibo2);
            sum += fibo2;
            fibo = fibo2;
            fibo2 = fibo3;
        }
        Debug.Log(sum);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -1 * bCount));        
    }

    int bCount = 0;
    public void Button()
    {
        bCount++;
        Debug.Log(bCount+"‰ñbutton‰Ÿ‚µ‚½");
        
    }
    
    public void Button2()
    {
        Debug.Log("button2");
    }
}

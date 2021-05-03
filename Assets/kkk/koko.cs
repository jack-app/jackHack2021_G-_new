using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koko : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int x;
        x = 3;
        x = x + 2;
        Debug.Log(x);
        if (x==3) {
            Debug.Log("x is 3");
        }
        else { Debug.Log("x is not 3"); }

        int sum = 0;
        for (int i = 0; i <10; i = i + 1) {
            sum = sum + i;
        }
        Debug.Log(sum);


        int s = 0;
        for (int h= 0; h < 101; h = h + 1) {   
            s = s + h;
        }
        Debug.Log(s);


        int j = 0;
        for (int g = 0; g < 11; g = g + 1){
            j = j + g*g;
        }
        Debug.Log(j);

    }


        
    




        

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Button()
    {
        Debug.Log("button");


    }

    public void BB()
    {
        int r;
        r = 4;
        Debug.Log(r);
        if (r==4)
        {
            Debug.Log("BB");
        }

        Debug.Log("kk");

        
    }
    
    
    

}
 
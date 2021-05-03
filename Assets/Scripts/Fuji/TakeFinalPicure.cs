using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeFinalPicure : MonoBehaviour
{
    Transform 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FinishSearchMode(int size)
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            Debug.Log(path);
            if(path != null)
            {
                Texture2D texture = NativeCamera.LoadImageAtPath(path, size);
                if(texture == null)
                {
                    Debug.Log("null");
                }
            }
        }
    }
}

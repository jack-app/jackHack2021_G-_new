using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScshoButton : MonoBehaviour
{

    [SerializeField] GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
            Invoke("Apper", 10);
        }
    }

    void Apper()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);

        }
    }
}

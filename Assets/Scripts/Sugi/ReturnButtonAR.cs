using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtonAR : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}

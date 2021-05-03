using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeInitializer : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAfterSceneLoad()
    {
        print("RubTimeInitializer");
        var audioSource = Instantiate(Resources.Load("AudioSource"));
        GameObject.DontDestroyOnLoad(audioSource);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmusubiKunAnimation : MonoBehaviour
{
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Found()
    {
            StartCoroutine(TurnAndHappy());
    }
    IEnumerator TurnAndHappy()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        myAnim.SetBool("Found", true);
        yield return new WaitForSeconds(1);
        myAnim.SetBool("Found", false);
        yield return new WaitForSeconds(1.458f);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}

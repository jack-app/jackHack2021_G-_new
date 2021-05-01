using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchMode_GPS : MonoBehaviour
{
    private TextMeshProUGUI tmPro;
    private Vector2 location;
    Utilities util;
    // Start is called before the first frame update
    private void Start()
    {
        tmPro = GameObject.Find("GPSText").GetComponent<TextMeshProUGUI>();
        util = GetComponent<Utilities>();
    }
    // Update is called once per frame
    public void ShowLocationForButton()
    {
        StartCoroutine(ShowLocation());
    }
    private IEnumerator ShowLocation()
    {
        yield return util.GPSCoroutine();
        location = (Vector2)util.GPSCoroutine().Current;
        tmPro.SetText(location.ToString());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchMode_GPS : MonoBehaviour
{
    private TextMeshProUGUI tmLocation,tmAngle;
    public Vector2 currentLocation;
    private float angle;
    // Start is called before the first frame update
    private void Start()
    {
        //Start������Find������
        tmLocation = GameObject.Find("LocationText").GetComponent<TextMeshProUGUI>();
        tmAngle = GameObject.Find("AngleText").GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    //�{�^���ŌĂт����̂�public void�̔�����Ԃ���
    public void ShowLocationForButton()
    {
        StartCoroutine(ShowLocation());
    }
    public void ShowAngleForButton()
    {
        StartCoroutine(ShowAngle());
    }
    private IEnumerator ShowLocation()
    {
        //Utilities�X�N���v�g����ʒu��������ĕ\���A��q�̊p�x������
        var locationNow = new Utilities().GPSCoroutine();
        yield return StartCoroutine(locationNow);
        currentLocation = (Vector2)locationNow.Current;
        tmLocation.SetText(currentLocation.ToString());
    }
    private IEnumerator ShowAngle()
    {
        var angleNow = new Utilities().DirectionCoroutine();
        yield return StartCoroutine(angleNow);
        angle = (float)angleNow.Current;
        tmAngle.SetText(angle.ToString());
    }
}

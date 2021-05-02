using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OmusubiAngleDetector : MonoBehaviour
{
    TextMeshProUGUI northText,southText,eastText,westText;
    public Vector2 currentLocation, nearestOmusubi;
    OmusubiGenerator OG;
    public float angle,currentDistance,sizeAdjust;
    public Transform omusubiKun;
    Vector3 omusubiKunStartPos;
    // Start is called before the first frame update
    void Start()
    {
        OG = GetComponent<OmusubiGenerator>();
        northText = GameObject.Find("North").GetComponent<TextMeshProUGUI>();
        southText = GameObject.Find("South").GetComponent<TextMeshProUGUI>();
        eastText = GameObject.Find("East").GetComponent<TextMeshProUGUI>();
        westText = GameObject.Find("West").GetComponent<TextMeshProUGUI>();
        omusubiKun = GameObject.Find("omusubi_").transform;
        omusubiKunStartPos = omusubiKun.transform.position;

        //�����X�V1�b�Ɉ��
        InvokeRepeating("CallDetectAngle", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallDetectAngle()
    {
        //if (OG.started == true)
        {
            StartCoroutine(DetectAngle());
        }
    }
    private IEnumerator DetectAngle()
    {
        var locationNow = new Utilities().GPSCoroutine();
        yield return StartCoroutine(locationNow);
        currentLocation = (Vector2)locationNow.Current;
        ShowAngle();
    }
    void ShowAngle()
    {
        //���ނ��у��X�g�̂����A�ł��߂����̂����o��
        float minDist = Mathf.Infinity;
        foreach (Vector2 omusubiLocation in OG.omusubiLocationList)
        {
            float distance = Utilities.GetDistanceFromGPS(currentLocation, omusubiLocation);

            if (distance < minDist)
            {
                currentDistance = distance;
                nearestOmusubi = omusubiLocation;
                minDist = distance;
            }
        }
        //�o�x�̍���
        float xDelta = currentLocation.x - nearestOmusubi.x;
        //�ܓx�̍���
        float yDelta = currentLocation.y - nearestOmusubi.y;
        //0.005�̎U��΂�
        Vector2 omusubiKunDirection = new Vector2(xDelta, yDelta).normalized;
        omusubiKun.position = new Vector3(omusubiKunDirection.x, omusubiKunDirection.y, 0)*currentDistance/200;
        //�����ɉ����Ă��ނ��т���̑傫�����ς��B
        //0.2-1.5�̊Ԃŋ����ɉ�����
        omusubiKun.localScale = new Vector3(Mathf.Min(Mathf.Max(0.2f, 50/currentDistance),1.5f),
                                            Mathf.Min(Mathf.Max(0.2f, 50 / currentDistance), 1.5f),
                                            Mathf.Min(Mathf.Max(0.2f, 50 / currentDistance), 1.5f));
    }
}

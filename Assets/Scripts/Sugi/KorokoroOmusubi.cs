using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KorokoroOmusubi : MonoBehaviour
{

    public Text text;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCorocoro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int distance=100;

    IEnumerator StartCorocoro()
    {

        var gpsCoroutine = GPSCoroutine();
        yield return StartCoroutine(gpsCoroutine);
        Vector2 gps = (Vector2)gpsCoroutine.Current;

        //Vector2 dir = new Vector2(Random.value - 0.5f, Random.value - 0.5f);
        Vector2 dir = new Vector2(-1, -1);

        dir = dir.normalized * distance;

        var targetGPS =gps+ Utilities.MeterToGPS(dir, gps.y);


        while (true)
        {


         


            var ComparingConnpassCoroutine = ComparingConnpass(targetGPS);


            yield return StartCoroutine(ComparingConnpassCoroutine);




            ////print((float)ComparingConnpassCoroutine.Current);



            var gpsc = GPSCoroutine();
            yield return StartCoroutine(gpsc);
            var nowGps = (Vector2)gpsc.Current;

            text.text= Utilities.GetDistanceFromGPS(nowGps, targetGPS)+"\n"+nowGps.x+"\n"+nowGps.y;


            SetOmusubi(targetGPS,nowGps);
            RotateCamera();
            yield return null;
        }


    }


    public GameObject Omusubi;

    
    void SetOmusubi(Vector2 targetGPS,Vector2 nowGPS)
    {
        Vector2 dir= Utilities.GPSToMeter(nowGPS, targetGPS);

        Vector2 omuPos = dir.normalized*10;

        Omusubi.transform.position = new Vector3(omuPos.x, 0, omuPos.y);

        float scale = distance/dir.magnitude;

        Omusubi.transform.localScale = Vector3.one * scale;

    }


    void RotateCamera()
    {
        Input.gyro.enabled = true;
        if (!Input.gyro.enabled) { return; }

        Quaternion correct = Quaternion.Euler(-90, 0, 0);
        Vector3 rotEuler = (correct * Input.gyro.attitude).eulerAngles;
        rotEuler.y *= -1;
        rotEuler.x *= -1;
        camera.transform.rotation = Quaternion.Euler(rotEuler);
    }

    /// <summary>
    /// �ܓx�ƌo�x��Ԃ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator GPSCoroutine()
    {
        int count = 0;
        while (!Input.location.isEnabledByUser)//GPS�@�\���L���ɂȂ�܂ő҂�
        {
            Debug.Log("GPS not enabled");
            yield return null;
            count++;
            if (count > 1000) { yield break; }
        }

        Input.location.Start(); //GPS�N���J�n

        while (Input.location.status != LocationServiceStatus.Running) //�N������������܂ő҂�
        {
            yield return null;
            count++;
            if (count > 1000) { yield break; }
        }

        print(Input.location.lastData.longitude);
        yield return new Vector2(Input.location.lastData.longitude, Input.location.lastData.latitude);

    }

    /* �g�p��
    public IEnumerator StartLocationService()
    {
        var coroutine = new Utilities().GPSCoroutine();
        yield return StartCoroutine(coroutine);
        Vector2 gps =(Vector2)coroutine.Current ;
        longitude = gps.x;
        latitude = gps.y;
    }
    */


    //�k����̊p�x��Ԃ�
    public IEnumerator DirectionCoroutine()
    {
        int count = 0;
        while (!Input.location.isEnabledByUser)//�R���p�X�@�\���L���ɂȂ�܂ő҂�
        {
            Debug.Log("GPS not enabled");
            yield return null;
            count++;
            if (count > 1000) { yield break; }
        }

        Input.location.Start(); //�R���p�X�N���J�n

        while (Input.location.status != LocationServiceStatus.Running) //�N������������܂ő҂�
        {
            yield return null;
            count++;
            if (count > 1000) { yield break; }
        }

        Input.compass.enabled = true; //�R���p�X�L����

        yield return Input.compass.trueHeading;
    }

    /* �g�p��
     * 
     public IEnumerator StartDirectionService()
    {
        var coroutine = new Utilities().DirectionCoroutine();
        yield return StartCoroutine(coroutine);
        print((float)coroutine.Current);
    }
     **/

    /// <summary>
    /// �R���p�X�̎w��������ږʍ��W�ŕԂ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetConnpassDirection()
    {
        var coroutine = DirectionCoroutine();
        yield return StartCoroutine(coroutine);
        float connpass = (float)coroutine.Current;

        Vector2 dir = new Vector2(Mathf.Sin(connpass * Mathf.PI / 180), Mathf.Cos(connpass * Mathf.PI / 180));

        yield return dir;
    }

    /// <summary>
    /// �ړI�n�ƃR���p�X�̕����̈�v����-1�`1�ŕԂ�
    /// </summary>
    /// <param name="targetGPS"></param>
    /// <returns></returns>
    public IEnumerator ComparingConnpass(Vector2 targetGPS)
    {
        var GPSCoroutine_ = GPSCoroutine();
        yield return StartCoroutine(GPSCoroutine_);

        Vector2 targetDir = Utilities.GPSToMeter((Vector2)GPSCoroutine_.Current, targetGPS);

        targetDir /= targetDir.magnitude;


        var connpassDirCoroutine = GetConnpassDirection();
        yield return StartCoroutine(connpassDirCoroutine);

        Vector2 connpassDir = (Vector2)connpassDirCoroutine.Current;


        yield return Vector3.Dot(targetDir, connpassDir);
    }


}


public class Destination
{
    Vector2 startGPS;


}

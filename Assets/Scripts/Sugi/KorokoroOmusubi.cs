using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KorokoroOmusubi : MonoBehaviour
{

    public Text text;
    public GameObject camera;
    public Image panel;
    public Vector2 targetGPS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartOmusubi()
    {
        StartCoroutine(ReadyCoroCoro());
    }


    public int distance=100;



    IEnumerator ReadyCoroCoro()
    {
        int count = 0;
        int speed = 0;
        while (count < 700)
        {
            Omusubi.transform.Rotate(0, 0, 6);

            if (count > 500)
            {
                Vector3 pos = Omusubi.transform.position;
                pos.z += speed*0.001f;
                speed += 1;
                Omusubi.transform.position = pos;
            }
            count++;
            yield return null;
        }


        count = 0;

        while (count < 200)
        {
            panel.color = new Color(1, 1, 1, count * 0.005f);
            count++;

            Omusubi.transform.Rotate(0, 0, 6);
            Vector3 pos = Omusubi.transform.position;
            pos.z += speed * 0.001f;
            speed += 1;
            Omusubi.transform.position = pos;
            yield return null;
        }
        count = 0;

        while (count < 40)
        {
            yield return null;
            count++;
        }


        Omusubi.GetComponent<Wa>().Cancel();
        Omusubi.GetComponent<OmuVoice>().VoicePlay();
        StartCoroutine(StartCorocoro());

        count = 0;

        while (count < 10)
        {
            yield return null;
            count++;
        }

        count = 0;

        while (count < 200)
        {
            panel.color = new Color(1, 1, 1, 1-count * 0.005f);
            count++;
            yield return null;
        }
    }




    public Vector2 startGPS;
    IEnumerator StartCorocoro()
    {

        var gpsCoroutine = GPSCoroutine();
        yield return StartCoroutine(gpsCoroutine);
        startGPS = (Vector2)gpsCoroutine.Current;

        //Vector2 dir = new Vector2(Random.value - 0.5f, Random.value - 0.5f);
        Vector2 dir = new Vector2(-1, -1);

        dir = dir.normalized * distance;

        targetGPS =startGPS+ Utilities.MeterToGPS(dir, startGPS.y);

        SetOmusubi(targetGPS, startGPS);

        while (true)
        {
            var ComparingConnpassCoroutine = ComparingConnpass(targetGPS);


            yield return StartCoroutine(ComparingConnpassCoroutine);




            ////print((float)ComparingConnpassCoroutine.Current);



            var gpsc = GPSCoroutine();
            yield return StartCoroutine(gpsc);
            var nowGps = (Vector2)gpsc.Current;

            text.text= Utilities.GetDistanceFromGPS(nowGps, targetGPS)+"\n"+nowGps.x+"\n"+nowGps.y;


            SetCamera(targetGPS,nowGps);
            RotateCamera();
            yield return null;


            if(Utilities.GetDistanceFromGPS(nowGps, targetGPS) < 50)
            {
                StartCoroutine(FindOmusubi());
                StopCoroutine(StartCorocoro());
            }
        }


    }


    IEnumerator FindOmusubi()
    {
        int count = 0;
        while(count<100)
        {
            panel.color = new Color(1, 1, 1, count * 0.01f);
            yield return null;
            count++;
        }

        while (count < 500)
        {
            yield return null;
            count++;
        }

        SceneManager.LoadScene("HelloAR");
    }


    public GameObject Omusubi;

    float magnification;
    const float OmusubiSetDistance = 200;
    void SetOmusubi(Vector2 targetGPS, Vector2 startGPS)
    {
        Vector2 dir = Utilities.GPSToMeter(startGPS, targetGPS);

        magnification = OmusubiSetDistance / distance;

        Vector2 omuPos = dir*magnification;

        Omusubi.transform.position = new Vector3(omuPos.x, 0, omuPos.y);
    }



    void SetCamera(Vector2 targetGPS,Vector2 nowGPS)
    {
        Vector2 dir= Utilities.GPSToMeter(startGPS, nowGPS);




        //Omusubi.transform.position = new Vector3(omuPos.x, 0, omuPos.y);

        Vector2 pos = dir * magnification;

        print(pos.x+","+pos.y);
        camera.transform.position = new Vector3(pos.x, camera.transform.position.y, pos.y);


        Omusubi.transform.rotation = Quaternion.LookRotation(Omusubi.transform.position - camera.transform.position);

        //float scale = distance/dir.magnitude;

        //Omusubi.transform.localScale = Vector3.one * scale;



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
    /// 緯度と経度を返す
    /// </summary>
    /// <returns></returns>
    public IEnumerator GPSCoroutine()
    {
        int count = 0;
        while (!Input.location.isEnabledByUser)//GPS機能が有効になるまで待つ
        {
            Debug.Log("GPS not enabled");
            yield return null;
            count++;
            if (count > 1000) { yield break; }
        }

        Input.location.Start(); //GPS起動開始

        while (Input.location.status != LocationServiceStatus.Running) //起動が完了するまで待つ
        {
            yield return null;
            count++;
            if (count > 1000) { yield break; }
        }

        //print(Input.location.lastData.longitude);
        yield return new Vector2(Input.location.lastData.longitude, Input.location.lastData.latitude);

    }

    /* 使用例
    public IEnumerator StartLocationService()
    {
        var coroutine = new Utilities().GPSCoroutine();
        yield return StartCoroutine(coroutine);
        Vector2 gps =(Vector2)coroutine.Current ;
        longitude = gps.x;
        latitude = gps.y;
    }
    */


    //北からの角度を返す
    public IEnumerator DirectionCoroutine()
    {
        int count = 0;
        while (!Input.location.isEnabledByUser)//コンパス機能が有効になるまで待つ
        {
            Debug.Log("GPS not enabled");
            yield return null;
            count++;
            if (count > 1000) { yield break; }
        }

        Input.location.Start(); //コンパス起動開始

        while (Input.location.status != LocationServiceStatus.Running) //起動が完了するまで待つ
        {
            yield return null;
            count++;
            if (count > 1000) { yield break; }
        }

        Input.compass.enabled = true; //コンパス有効化

        yield return Input.compass.trueHeading;
    }

    /* 使用例
     * 
     public IEnumerator StartDirectionService()
    {
        var coroutine = new Utilities().DirectionCoroutine();
        yield return StartCoroutine(coroutine);
        print((float)coroutine.Current);
    }
     **/

    /// <summary>
    /// コンパスの指す方向を接面座標で返す
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
    /// 目的地とコンパスの方向の一致率を-1～1で返す
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

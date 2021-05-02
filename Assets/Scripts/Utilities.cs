using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{

    public static Utilities utilities;
    public static Utilities Instance
    {
        get
        {
            if (utilities == null)
            {
                 utilities = new GameObject("Util",typeof(Utilities)).GetComponent<Utilities>();
            }

            return utilities;
        }
    }




    public const int meter_per_longitude_X_equator = 111316;
    public const int meter_per_latitude_Y = 110942;

    /// <summary>
    /// ある緯度における単位経度あたりの距離をメートルでかえす
    /// </summary>
    /// <param name="latitudeY"></param>
    /// <returns></returns>
    public static int MeterPerLongitudeX(float latitudeY)
    {
        return (int)(meter_per_longitude_X_equator * Mathf.Cos(latitudeY * Mathf.PI / 180));
    }

    //ある緯度における方向(メートル単位)を緯度経度に直す
    public static Vector2 MeterToGPS(Vector2 vec, float latitudeY)
    {
        float lonX = vec.x / (MeterPerLongitudeX(latitudeY));
        float latY = vec.y / meter_per_latitude_Y;

        return new Vector2(lonX, latY);
    }

    //2つのGPS座標を結ぶベクトルをメートルに直す
    public static Vector2 GPSToMeter(Vector2 gpsFrom, Vector2 gpsTo)
    {
        var dirLonLat = gpsTo - gpsFrom;
        Vector2 dir = new Vector2();

        dir.x = dirLonLat.x * MeterPerLongitudeX(gpsFrom.y);
        dir.y = dirLonLat.y * meter_per_latitude_Y;

        return dir;
    }


    //2つの緯度経度間の距離を出す
    public static float GetDistanceFromGPS(Vector2 gps1, Vector2 gps2)
    {
        return GPSToMeter(gps1, gps2).magnitude;
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

        print(Input.location.lastData.longitude);
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

        Vector2 targetDir = GPSToMeter((Vector2)GPSCoroutine_.Current, targetGPS);

        targetDir /= targetDir.magnitude;


        var connpassDirCoroutine = GetConnpassDirection();
        yield return StartCoroutine(connpassDirCoroutine);

        Vector2 connpassDir = (Vector2)connpassDirCoroutine.Current;


        yield return Vector3.Dot(targetDir, connpassDir);
    }


}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
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

        return new Vector2(lonX,latY);
    }

    


    //2つの緯度経度間の距離を出す
    public static float GetDistanceFromGPS(Vector2 gps1,Vector2 gps2)
    {
        var dirLonLat = gps2 - gps1;
        Vector2 dir = new Vector2();

        dir.x = dirLonLat.x * MeterPerLongitudeX(gps1.y);
        dir.y = dirLonLat.y * meter_per_latitude_Y;

        return dir.magnitude;
    }


    /// <summary>
    /// 緯度と経度を返す
    /// </summary>
    /// <returns></returns>
    public IEnumerator  GPSCoroutine()
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




}




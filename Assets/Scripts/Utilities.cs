using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
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




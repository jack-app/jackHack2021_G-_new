using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public const int meter_per_longitude_X_equator = 111316;
    public const int meter_per_latitude_Y = 110942;

    /// <summary>
    /// ����ܓx�ɂ�����P�ʌo�x������̋��������[�g���ł�����
    /// </summary>
    /// <param name="latitudeY"></param>
    /// <returns></returns>
    public static int MeterPerLongitudeX(float latitudeY)
    {
        return (int)(meter_per_longitude_X_equator * Mathf.Cos(latitudeY * Mathf.PI / 180));
    }

    //����ܓx�ɂ��������(���[�g���P��)���ܓx�o�x�ɒ���
    public static Vector2 MeterToGPS(Vector2 vec, float latitudeY)
    {
        float lonX = vec.x / (MeterPerLongitudeX(latitudeY));
        float latY = vec.y / meter_per_latitude_Y;

        return new Vector2(lonX,latY);
    }

    


    //2�̈ܓx�o�x�Ԃ̋������o��
    public static float GetDistanceFromGPS(Vector2 gps1,Vector2 gps2)
    {
        var dirLonLat = gps2 - gps1;
        Vector2 dir = new Vector2();

        dir.x = dirLonLat.x * MeterPerLongitudeX(gps1.y);
        dir.y = dirLonLat.y * meter_per_latitude_Y;

        return dir.magnitude;
    }


    /// <summary>
    /// �ܓx�ƌo�x��Ԃ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator  GPSCoroutine()
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




}




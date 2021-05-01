using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
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




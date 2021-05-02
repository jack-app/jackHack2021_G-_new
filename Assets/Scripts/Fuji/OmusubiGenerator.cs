using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OmusubiGenerator : MonoBehaviour
{
    private Image loadScreen;
    private Vector2 startLocation;
    public bool started = false;
    public int numberOfOmusubi;
    public List<Vector2> omusubiLocationList;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        loadScreen = GameObject.Find("LoadScreen").GetComponent<Image>();
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    /*
    public void CallStartGame()
    {
        //1�񂵂��ĂׂȂ��悤��Bool
        if (started == false)
        {
            StartCoroutine(StartGame());
            started = true;
        }
    }
    */
    private IEnumerator StartGame()
    {
        //���݈ʒu���擾���Ă��ނ��т�u��SetOmusubi
        var locationNow = new Utilities().GPSCoroutine();
        yield return StartCoroutine(locationNow);
        startLocation = (Vector2)locationNow.Current;
        SetOmusubi();
        while (true)
        {
            yield return null;
            Color c = loadScreen.color;
            c.a -= 0.01f;
            loadScreen.color = c;
            if(c.a <= 0)
            {
                c.a = 0;
                loadScreen.color = c;
                loadScreen.enabled = false;
                break;
            }
        }
    }
    void SetOmusubi()
    {
        //���ނ��т̐��������݈ʒu����0.005���̕��Ń����_���ɂ��ނ��т�u��float������_�u��Ȃ��ł���(�K��)
        for(int i = 0; i < numberOfOmusubi; i++)
        {
            omusubiLocationList.Add(new Vector2(startLocation.x + Random.Range(-0.005f,0.005f), startLocation.y + Random.Range(-0.005f, 0.005f)));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    private float advanceBullet = 25f;
    public Vector3 direction;
    //public AudioClip audioClip1;
    public float distance;
    //private AudioSource audioSource;
    private Vector3 passing_point1;
    private float min_distance1=1000;
    BulletGenerator bulletGeneratorScript;
    AvatarMove avatarMove;
    TextManager textManager;
    LineRenderer line;
    private string time;
    private string time1;
    private string time2;
    GameObject SaveCsv;
    GameObject text;
    CsvScript csvScript;
    SaveAnswerCsv answerScript;
    public float radius = 0.037f;
    private bool passing = false; //頭のそばを通過したか

    // Start is called before the first frame update
    void Start()
    {
        SaveCsv = GameObject.Find("SaveCsv");
        csvScript = SaveCsv.GetComponent<CsvScript>();
        answerScript = SaveCsv.GetComponent<SaveAnswerCsv>();
        GameObject generator = GameObject.Find("BulletLauncher");
        bulletGeneratorScript = generator.GetComponent<BulletGenerator>();
        GameObject avatar = GameObject.Find("head");
        avatarMove = avatar.GetComponent<AvatarMove>();
        text = GameObject.Find("Text");
        textManager = text.GetComponent<TextManager>();
        int number = bulletGeneratorScript.num;
        direction = new Vector3(0f, 0f, -1.0f);
        direction = direction / direction.magnitude;
        advanceBullet = bulletGeneratorScript.speed;
        DateTime t = DateTime.Now;
        time1 = string.Format("{0:yyyy-MM-dd HH:mm:ss.f}",t); //time
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.transform.position);
        //print("FPS=" + 1 / Time.deltaTime);
        DateTime t = DateTime.Now;
        time = string.Format("{0:yyyy-MM-dd HH:mm:ss.f}",t); //time
        transform.position += direction * advanceBullet * Time.deltaTime; //球の移動
        distance = avatarMove.CalPassingPoints(this.transform.position); //球の中心からアバターのコライダーの距離を測定
        distance = distance - radius; // 球の輪郭からの距離にするため距離から球の半径分を引く
        //Debug.Log(min_distance1); //現時点の最短距離
        if (min_distance1 > distance)//最短通過点なら
        {
            time2 = time;
            passing_point1 = this.transform.position;
            min_distance1 = distance;
        }else if(!passing){
            csvScript.SaveData(bulletGeneratorScript.launching_point.ToString("F4"), time1, min_distance1.ToString(), passing_point1.ToString("F4"), time2, advanceBullet.ToString());
            passing = true;
            textManager.scene=2;
        }
        if (passing)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.B) || OVRInput.GetDown(OVRInput.RawButton.Y) || OVRInput.GetDown(OVRInput.RawButton.X)|| Input.GetKeyDown(KeyCode.Y))
            {
                answerScript.SaveAnswerData(bulletGeneratorScript.launching_point.ToString("F4"), time, textManager.close.ToString(), advanceBullet.ToString());
                Destroy(gameObject);
            }
        }
    }
}

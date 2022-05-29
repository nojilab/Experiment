using System;
using System.IO;
using System.Text;
using System.Collections;
using UnityEngine;

public class SaveCsvHR : MonoBehaviour
{
    public GameObject heartRateDisplay;
    public HeartRateDisplay heartRateDisplayScript;
    private StreamWriter sw;
    private string datetimeStr;
    private float time;
    private float span = 0.1f;
    private float heartrate;
    // Start is called before the first frame update
    void Start()
    {
        heartRateDisplayScript = heartRateDisplay.GetComponent<HeartRateDisplay>(); 
        time = 0f;
        sw = new StreamWriter(@"SaveDataHR.csv",true, Encoding.GetEncoding("Shift_JIS"));
        string[] s1 = { "HR", "world time", "elapsed time"};
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        StartCoroutine("Logging");
    }

    public void SaveData(float heartRate, string txt1, string txt2){
        string[] s1 = {heartRate.ToString(), txt1, txt2};
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    // Update is called once per frame
    void Update()
    {
        heartrate = heartRateDisplayScript.heartRate;
        time += Time.deltaTime; //1フレームのtime追加
        DateTime datetime = System.DateTime.Now; //現在時刻
        datetimeStr = datetime.ToString()+datetime.Millisecond.ToString(); //現在時刻string変換
        if(Input.GetKeyDown(KeyCode.Return)){
            sw.Close();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
    IEnumerator Logging(){
        while(true){
            yield return new WaitForSeconds(span);
            SaveData(heartrate, datetimeStr, time.ToString()); //Save csv 1 line
        }
    }
}
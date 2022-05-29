using System.Collections;
using System;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveTrackingCsv : MonoBehaviour
{
    public GameObject CameraRig;
    private StreamWriter sw;
    private float time;
    private float span = 0.1f;
    private string datetimeStr;
   // Start is called before the first frame update
    void Start()
    {
        sw = new StreamWriter(@"TrackingData.csv", true, Encoding.GetEncoding("Shift_JIS"));
        string[] s1 = { "trackingx", "y", "z", "timestamp" };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        StartCoroutine("Logging");
    }

    public void SaveData(string xyz1 , string time1){
        string[] s1 = { xyz1, time1};
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    // Update is called once per frame
    void Update()
    {
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
            SaveData(CameraRig.transform.position.ToString("F4"), datetimeStr); //Save csv 1 line
        }
    }
}

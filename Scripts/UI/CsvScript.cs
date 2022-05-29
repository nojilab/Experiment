using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class CsvScript : MonoBehaviour
{
    private StreamWriter sw;
    [SerializeField] private int limit;
    BulletGenerator bulletGeneratorScript;
    private bool end = false;
    public string filename = "C:/research/Experiment2021/SaveData2022/Kashimura/Bullet/BulletData.csv";
    // Start is called before the first frame update
    void Start()//ヘッダー出力
    {
        GameObject generator = GameObject.Find("BulletLauncher");
        bulletGeneratorScript = generator.GetComponent<BulletGenerator>();
        limit = bulletGeneratorScript.limit;
        sw = new StreamWriter(filename, true, Encoding.GetEncoding("Shift_JIS"));
        string[] s1 = { "x", "y", "z", "launchingtime","distance","minx","y","z", "approachingtime","speed" };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    public void SaveData(string xyz1, string time1, string xyz2, string distance, string time2, string speed)//セーブ関数
    {
        Debug.Log("BulletSave");
        string[] s1 = { xyz1, time1 , xyz2, distance, time2, speed};
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            sw.Write(System.Environment.NewLine);
            Debug.Log("SAVE");
            sw.Close();
            UnityEditor.EditorApplication.isPlaying= false;
        }
        if (bulletGeneratorScript.num == limit+1 && !end)
        {
            sw.Write(System.Environment.NewLine);
            Debug.Log("save"+limit+"points.");
            sw.Close();
            end = true;
        }
    }
}

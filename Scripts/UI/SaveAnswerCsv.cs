using UnityEngine;
using System.IO;
using System.Text;

public class SaveAnswerCsv : MonoBehaviour
{
    private StreamWriter sw;
    BulletGenerator bulletGeneratorScript;
    // Start is called before the first frame update
    [SerializeField] private int limit;
    private bool end = false;
    public string filename = "C:/research/Experiment2021/SaveData2022/Kashimura/Answer/AnswerData.csv";
    void Start()//ヘッダー出力
    {
        GameObject generator = GameObject.Find("BulletLauncher");
        bulletGeneratorScript = generator.GetComponent<BulletGenerator>();
        limit = bulletGeneratorScript.limit;
        sw = new StreamWriter(filename, true, Encoding.GetEncoding("Shift_JIS"));
        string[] s1 = { "launchingx", "y", "z", "answertime", "answer","speed" };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    public void SaveAnswerData(string xyz1, string time1, string ans, string speed)//セーブ関数
    {
        Debug.Log("AnswerSave");
        string[] s1 = { xyz1, time1 , ans, speed};
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

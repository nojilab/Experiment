using System.Collections;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
 
    public GameObject BulletPrefabs; //Prefab(Bullet)
    public GameObject LaunchingPointPrefabs; //Prefab(発射点の印)
    public int num = 0; //発射した弾の数
    public int limit = 60; //1セットの回数
    public Vector3 launching_point; //発射点
    public float hmd_height; //hmdの高さ
    private Vector3[] vector3s = new Vector3[66]; //発射点のプール
    private float[] speeds = { 1f, 5f, 12.5f}; //スピードのプール
    public float speed;
    // Start is called before the first frame update
    async void Start()
    {
        float i = -0.6f;
        int j;
        for (j = 0; j < 22; j++)
        {
            if(j == 11) { Debug.Log("check");  i = 0.1f; }
            vector3s[j] = new Vector3(i, 1.7f, 2f);
            vector3s[j+22] = new Vector3(i, 1.7f, 10f);
            vector3s[j+44] = new Vector3(i, 1.7f, 25f);
    //        GameObject instance = (GameObject)Instantiate(LaunchingPointPrefabs, vector3s[j], Quaternion.identity);
            Debug.Log(j);
            Debug.Log(vector3s[j].ToString("F4"));
            i += 0.05f;
        }
        Random.InitState(System.DateTime.Now.Millisecond);
        StartCoroutine(WaitCoroutine());
    }

    // Update is called once per frame

    private IEnumerator WaitCoroutine(){
        yield return new WaitUntil(() => (OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.B)|| OVRInput.GetDown(OVRInput.RawButton.X) || OVRInput.GetDown(OVRInput.RawButton.Y)|| Input.GetKeyDown(KeyCode.Y)));
        if(num <limit){
            StartCoroutine(DelayCoroutine());
        }else{
            num += 1;
        }
    }
    private IEnumerator DelayCoroutine(){
        //int i = Random.Range(4,6); //4~6秒ランダム
        yield return new WaitForSeconds(6); //5秒待つ
        int random_pos = Random.Range(0,22);
        int random_spd = Random.Range(0,3);
        this.num += 1;
        GameObject item = Instantiate(BulletPrefabs) as GameObject; //call Prefab
        //launching_point = new Vector3(0,1.4f, 18.44f);
        speed = speeds[random_spd];
        if(speed == 1f){
            launching_point = vector3s[random_pos];
        }else if(speed == 5f){
            launching_point = vector3s[random_pos+22];
        }else if(speed == 12.5f){
            launching_point = vector3s[random_pos+44];
        }
        item.transform.position = launching_point; //generate
        Debug.Log(launching_point);
        Debug.Log(speed);
        Debug.Log(num);
        StartCoroutine(WaitCoroutine());
    }
}

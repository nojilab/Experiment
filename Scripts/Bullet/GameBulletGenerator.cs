using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBulletGenerator : MonoBehaviour
{
    public GameObject BulletPrefabs; //Prefab(Bullet)

    public GameObject BombPrefabs;
    public GameObject LaunchingPointPrefabs; //Prefab(発射点の印)
    GameObject item;
    public int num = 0; //発射した弾の数
    public int limit = 20; //1セットの回数
    public Vector3 launching_point; //発射点
    public float hmd_height; //hmdの高さ
    private Vector3[] vector3s = new Vector3[20]; //発射点のプール
    private float[] speeds = { 6.25f, 12.5f, 25f}; //スピードのプール
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        float i = -20f;
        int j;
        for (j = 0; j < 20; j++)
        {
            vector3s[j] = new Vector3(i, 1.7f, 20f);
            Debug.Log(j);
            Debug.Log(vector3s[j].ToString("F4"));
            i += 2f;
        }
    }

    // Update is called once per frame
    void Update(){
        if(OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.B) || Input.GetKeyDown(KeyCode.Y)){
            int random_pos = Random.Range(0,20);
            int random_spd = Random.Range(0,3);
            int random_bullet = Random.Range(0,3);
            launching_point = vector3s[random_pos];
            this.num += 1;
            if(random_bullet < 2){
                item = Instantiate(BulletPrefabs) as GameObject; //call Prefab
                speed = speeds[random_spd];
                item.transform.position = launching_point; //generate
            }else{
                item = Instantiate(BombPrefabs) as GameObject; //call Prefab
                speed = speeds[random_spd];
                item.transform.position = launching_point; //generate               
            }
            Debug.Log(launching_point);
            Debug.Log(speed);
            Debug.Log(num);
        }
    }
}

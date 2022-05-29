using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBulletController : MonoBehaviour
{
    private float advanceBullet = 25f;
    public Vector3 direction;
    //public AudioClip audioClip1;
    //private AudioSource audioSource;
    GameBulletGenerator bulletGeneratorScript;
    FlushController flushController;
    private Vector3[] vector3s = new Vector3[20];

    // Start is called before the first frame update
    void Start()
    {
        float i = -0.2f;
        int j;
        for (j = 0; j < 20; j++)
        {
            vector3s[j] = new Vector3(i, 1.7f, 0f);
            i += 0.02f;
        }
        GameObject generator = GameObject.Find("BulletLauncher");
        bulletGeneratorScript = generator.GetComponent<GameBulletGenerator>();
        GameObject flush = GameObject.Find("FlushController");
        flushController = flush.GetComponent<FlushController>();
        int random_pos = Random.Range(0,20);
        int number = bulletGeneratorScript.num;
        direction = vector3s[random_pos] - bulletGeneratorScript.launching_point;
        direction = direction / direction.magnitude;
        Debug.Log(bulletGeneratorScript.launching_point);
        Debug.Log(vector3s[random_pos]);
        Debug.Log(direction);
        advanceBullet = bulletGeneratorScript.speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * advanceBullet * Time.deltaTime; //球の移動
        if (this.transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            flushController.Damage();
            Destroy(other.gameObject);
        }
    }
}

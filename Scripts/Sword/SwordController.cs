using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject ParticlePrefabs;

    public GameObject ExprosionPrefabs;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            GameObject particle = Instantiate(ParticlePrefabs) as GameObject;
            Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
            particle.transform.position = hitPos;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Bomb"){
            GameObject particle = Instantiate(ExprosionPrefabs) as GameObject;
            Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
            particle.transform.position = hitPos;
            Destroy(other.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

    }
}

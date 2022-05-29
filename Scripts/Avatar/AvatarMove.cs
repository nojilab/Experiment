using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AvatarMove : MonoBehaviour
{
    public Collider[] colls;
    public Transform headTransform;
    private Vector3 basePos = new Vector3(0.0f, 1.7f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        colls = this.gameObject.GetComponentsInChildren<CapsuleCollider>();
    }
    public float CalPassingPoints(Vector3 passingPos)
    {
        float min = 100;
        foreach (var coll in colls)
        {
            Vector3 closestPoint = coll.ClosestPointOnBounds(passingPos);
            float distance = Vector3.Distance(closestPoint, passingPos);
            if (distance < min)
            {
                min = distance;
            }
        }

        return min;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //this.transform.position = basePos;
        //this.transform.position = headTransform.position;
    }
}

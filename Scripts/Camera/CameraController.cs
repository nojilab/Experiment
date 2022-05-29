using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CameraPos; 
    Transform CameraTrans;
    void Start()
    {
        CameraTrans = CameraPos.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = CameraTrans.position;
        if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger)){
            if(pos.x < 20f){
                pos.x += 0.1f;
                CameraTrans.position = pos;
            }
        }else{
            if(pos.x > 0f){
                pos.x -= 0.1f;
                CameraTrans.position = pos;
            }
        }
        if(OVRInput.Get(OVRInput.RawButton.LIndexTrigger)){
            if(pos.x > -20f){
                pos.x += 0.1f;
                CameraTrans.position = pos;
            }
        }else{
            if(pos.x < 0f){
                pos.x -= 0.1f;
                CameraTrans.position = pos;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float cameraSpeed=3,cameraAcceleration=1,deadZone=4;
    //camera movement should be always greater than player speed
    [SerializeField]
    Vector3 cameraOffset;
    [SerializeField]
    Transform target;
    float currentSpeed;
    void Update()
    {
        Vector3 delta=target.position-transform.position;
        if(Mathf.Abs(delta.x)>deadZone || Mathf.Abs(delta.y)>deadZone){
            currentSpeed=Mathf.MoveTowards(currentSpeed,cameraSpeed,cameraAcceleration*Time.deltaTime);
        }else
        {
            currentSpeed=Mathf.MoveTowards(currentSpeed,0,cameraAcceleration*Time.deltaTime);
        }
        transform.position=Vector3.MoveTowards(transform.position,target.position+cameraOffset,currentSpeed*Time.deltaTime);
    }
}

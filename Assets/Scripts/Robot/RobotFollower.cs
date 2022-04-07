using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFollower : MonoBehaviour
{
    public Transform target;
    public float speed;

    void Update()
    {
        if(Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        }
        
        var rot = Quaternion.LookRotation(target.position - transform.position, Vector3.up).eulerAngles;
        rot.x = Mathf.Clamp(rot.x, -16, 16);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rot), speed * Time.deltaTime * 3);
    }
}

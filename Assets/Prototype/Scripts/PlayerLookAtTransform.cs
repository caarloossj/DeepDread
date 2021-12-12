using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtTransform : MonoBehaviour
{
    public Transform playerLookAt;
    public bool freeze = false;
    public float XZspeed;
    public float Yspeed;


    void LateUpdate()
    {
        Vector3 smoothedPosition = new Vector3();

        if (!freeze)
        {
            smoothedPosition.x = Mathf.Lerp(transform.position.x, playerLookAt.position.x, Time.deltaTime * XZspeed);
            smoothedPosition.z = Mathf.Lerp(transform.position.z, playerLookAt.position.z, Time.deltaTime * XZspeed);
        } else
        {
            smoothedPosition.x = transform.position.x;
            smoothedPosition.z = transform.position.z;
        }

        smoothedPosition.y = Mathf.Lerp(transform.position.y, playerLookAt.position.y, Time.deltaTime * Yspeed);


        transform.position = smoothedPosition;
    }

    public IEnumerator Freeze (float duration)
    {
        freeze = true;

        yield return new WaitForSeconds(duration);

        freeze = false;
    }
}

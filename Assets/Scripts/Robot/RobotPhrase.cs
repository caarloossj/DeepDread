using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPhrase : MonoBehaviour
{
    public string phrase;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Robot"))
        {
            other.GetComponent<FloatingBox>().Popup(phrase);
            Destroy(gameObject);
        }
    }
}

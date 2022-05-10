using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent checkpointEvent;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            checkpointEvent.Invoke();
            Destroy(gameObject);
        }
    }
}

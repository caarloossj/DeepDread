using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Terminal : MonoBehaviour
{
    public UnityEvent terminalEvent;
    public GameObject interactText;
    private bool playerIN;

    private void OnEnable() {
        ActionCharacter.Instance.playerInput.CharacterControls.Interact.performed += conext => Interact();
    }

    private void OnDisable() {
        ActionCharacter.Instance.playerInput.CharacterControls.Interact.performed -= conext => Interact();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            interactText.SetActive(true);
            playerIN = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            interactText.SetActive(false);
            playerIN = false;;
        }
    }

    private void Interact()
    {
        if(!playerIN) return;
        interactText.SetActive(false);
        terminalEvent.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Terminal : MonoBehaviour
{
    public UnityEvent terminalEvent;
    public GameObject interactText;
    private bool playerIN;
    public bool can = true;

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
            playerIN = false;
        }
    }

    public void EnableTrigger()
    {
        can = true;
    }

    private void Interact()
    {
        if(!playerIN) return;
        if(can == false)
        {
            FindObjectOfType<FloatingBox>().Popup("Primero deberias coger esa espada");
            return;
        }
        interactText.SetActive(false);
        terminalEvent.Invoke();
    }

    public void TravelToAfter(float seconds)
    {
        //8 Seconds
        StartCoroutine(travel());
        IEnumerator travel()
        {
            yield return new WaitForSeconds(seconds);
            SceneManager.LoadScene("Pre_Boss_Room");
        }
    }
}

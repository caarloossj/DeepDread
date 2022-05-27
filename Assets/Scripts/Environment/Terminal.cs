using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Terminal : MonoBehaviour
{
    public UnityEvent terminalEvent;
    public GameObject interactText;
    private bool playerIN;
    public bool can = true;
    public AudioClip clip;
    public float volume = 0.5f;

    private void OnEnable() {
        ActionCharacter.Instance.playerInput.CharacterControls.Interact.performed += Interact;
    }

    private void OnDisable() {
        ActionCharacter.Instance.playerInput.CharacterControls.Interact.performed -= Interact;
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

    private void Interact(InputAction.CallbackContext context)
    {
        if(!playerIN) return;
        if(can == false)
        {
            FindObjectOfType<FloatingBox>().Popup("Primero deberias coger esa espada");
            return;
        }
        FindObjectOfType<GlobalAudioManager>().AudioPlay(clip, volume, false, transform.position, true); ;
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

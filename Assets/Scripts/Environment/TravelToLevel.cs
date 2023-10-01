using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;

public class TravelToLevel : MonoBehaviour
{
    public string levelName;
    public GameObject interactText;
    public CinemachineStoryboard storyboard;
    public bool blocked = false;
    private bool playerIN;
    public AudioClip clip;

    private void OnEnable() {
        ActionCharacter.Instance.playerInput.CharacterControls.Interact.performed += Interact;
    }

    private void OnDisable() {
        if(ActionCharacter.Instance != null)
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

    public void Unlock(string text)
    {
        interactText.GetComponent<TextMeshProUGUI>().text = "Presiona 'E' para interactuar";
        blocked = false;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(!playerIN || blocked) return;
        interactText.SetActive(false);
        FindObjectOfType<GlobalAudioManager>().AudioPlay(clip, 0.5f, false, transform.position, true); ;
        DOTween.To(() => storyboard.m_Alpha,
            x =>
            {
                storyboard.m_Alpha = x;
            },
            1, 1f).SetEase(Ease.Linear).OnComplete(() => SceneManager.LoadScene(levelName));
        
    }
}

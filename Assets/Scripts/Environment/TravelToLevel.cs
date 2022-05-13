using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using DG.Tweening;

public class TravelToLevel : MonoBehaviour
{
    public string levelName;
    public GameObject interactText;
    public CinemachineStoryboard storyboard;
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
            playerIN = false;
        }
    }

    private void Interact()
    {
        if(!playerIN) return;
        interactText.SetActive(false);
        DOTween.To(() => storyboard.m_Alpha,
            x =>
            {
                storyboard.m_Alpha = x;
            },
            1, 1f).SetEase(Ease.Linear).OnComplete(() => SceneManager.LoadScene(levelName));
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public bool isPause = false;
    public GameObject pauseMenu;
    public PlayableDirector deathTimeline;
    public Transform acidFX;

    #region Singleton
    //Singleton
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    private static GameManager _instance;
    #endregion

    // Update is called once per frame
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void Die(int type){
        switch(type)
        {
            case 0:
                var acid = Instantiate(acidFX, ActionCharacter.Instance.transform.position + Vector3.up*0.3f, Quaternion.identity);
                Destroy(acid.gameObject, 1.5f);
                break;
        }
        ActionCharacter.Instance.dead = true;
        ActionCharacter.Instance.currentMovement = Vector3.zero;
        ActionCharacter.Instance.animator.SetBool("dead", true);
        deathTimeline.Play();
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine() {
        yield return new WaitForSeconds(5);
        Respawn();
    }

    private void Respawn() {
        ActionCharacter.Instance.dead = false;
        ActionCharacter.Instance.animator.SetBool("dead", false);
    }

    public void SwitchPause()
    {
        isPause = !isPause;

        if(isPause) {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ActionCharacter.Instance.EnableInput(false);
        } else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ActionCharacter.Instance.EnableInput(true);
        }
    }
}

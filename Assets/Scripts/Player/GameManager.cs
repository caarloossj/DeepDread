using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public bool isPause = false;
    public GameObject pauseMenu;
    public PlayableDirector deathTimeline;
    public Transform acidFX;
    public Transform currentCheckPoint;
    public float totalLife = 100;
    public float currentLife = 100;
    public Image lifebar;
    public Image staminabar;
    public Image blood;

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
    public float acidHeight;
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
                var pos = ActionCharacter.Instance.transform.position;
                pos.y = acidHeight;
                var acid = Instantiate(acidFX, pos, Quaternion.identity);
                Destroy(acid.gameObject, 1.5f);
                lifebar.DOFillAmount(0, 0.4f).SetEase(Ease.InQuad);
                break;
        }
        if(ActionCharacter.Instance.targetLocked !=null) ActionCharacter.Instance.TargetLock();
        ActionCharacter.Instance.dead = true;
        ActionCharacter.Instance.currentMovement = Vector3.zero;
        ActionCharacter.Instance.animator.SetBool("dead", true);
        deathTimeline.Play();
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine() {
        yield return new WaitForSeconds(4);
        ActionCharacter.Instance.transform.position = currentCheckPoint.position;
        yield return new WaitForSeconds(1);
        Respawn();
    }

    public void LifeBar(float life)
    {
        currentLife -= life;
        lifebar.DOFillAmount(currentLife/100f, 0.3f).SetEase(Ease.InQuad);
        lifebar.DOBlendableColor(Color.red, 0.1f).OnComplete(() => lifebar.DOBlendableColor(Color.white, 0.3f));
        blood.DOFade(0.2f, 0.1f).OnComplete(() => blood.DOFade(0, 0.7f));
        if(currentLife <= 0)
        {
            Die(1);
        }
    }

    public void ChangeCheckpoint(Transform newt)
    {
        currentCheckPoint = newt;
    }

    private void Respawn() {
        ActionCharacter.Instance.dead = false;
        ActionCharacter.Instance.animator.SetBool("dead", false);
        lifebar.DOFillAmount(1, 0.5f).SetEase(Ease.OutQuad);
        currentLife = totalLife;
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

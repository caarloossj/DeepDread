using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using Cinemachine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public bool isPause = false;
    public GameObject pauseMenu;
    public PlayableDirector deathTimeline;
    public Transform acidFX;
    public Transform healFX;
    public Transform currentCheckPoint;
    public Animator ianAnimator;
    public float totalLife = 100;
    public float currentLife = 100;
    public float currentStamina = 100;
    public Image lifebar;
    public Image staminaBar;
    public int healthCharge = 3;
    public Transform chargeParent;
    public Image noStamina;
    private bool lossingStamina;
    public Button defaultButton;
    public GameObject hud;
    public CinemachineVirtualCamera cam;
    private Tween staminaTween;
    public Image blood;
    public bool firstEnemy = false;

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
    public Color orangeColor;
    public float staminaSpeed = 3;
    #endregion

    // Update is called once per frame
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(!lossingStamina && currentStamina < 100)
        {
            currentStamina += Time.deltaTime * staminaSpeed;
            staminaBar.fillAmount = currentStamina/100f;
        }
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
                LifeBar(30);
                break;
        }
        if(ActionCharacter.Instance.targetLocked !=null) ActionCharacter.Instance.TargetLock();
        ActionCharacter.Instance.dead = true;
        ActionCharacter.Instance.currentMovement = Vector3.zero;
        ActionCharacter.Instance.animator.SetBool("dead", true);
        deathTimeline.Play();
        StartCoroutine(RespawnCoroutine());

        if(TryGetComponent<WaveHack>(out var hack) && currentLife <= 0)
        {
            hack.ResetHack();
        }
    }

    private IEnumerator RespawnCoroutine() {
        yield return new WaitForSeconds(4);
        ActionCharacter.Instance.transform.position = currentCheckPoint.position;
        yield return new WaitForSeconds(1);
        Respawn(currentLife <= 0);
    }

    public void Heal()
    {
        if(healthCharge<=0) return;

        healthCharge--;

        chargeParent.GetChild(healthCharge).gameObject.SetActive(false);

        ianAnimator.SetTrigger("heal");
        currentLife += 35;
        currentLife = Mathf.Clamp(currentLife, 0, 100);
        lifebar.DOFillAmount(currentLife/100f, 0.3f).SetEase(Ease.InQuad);
        lifebar.DOBlendableColor(Color.green, 0.1f).OnComplete(() => lifebar.DOBlendableColor(Color.white, 0.3f));
        Destroy(Instantiate(healFX, ActionCharacter.Instance.transform).gameObject, 3);
        healFX.localPosition = new Vector3(0, 0.6f, 0);
    }

    public void Stamina(float stamina)
    {
        if(staminaTween != null)
            staminaTween.Kill(true);

        currentStamina -= stamina;
        lossingStamina = true;
        staminaTween = staminaBar.DOFillAmount(currentStamina/100f, 0.3f).SetEase(Ease.InQuad).OnComplete(()=>staminaTween= null);
        staminaBar.DOBlendableColor(orangeColor, 0.1f).OnComplete(() => {staminaBar.DOBlendableColor(Color.white, 0.3f); lossingStamina = false;});
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

    public void NoStamina()
    {
        noStamina.DOFade(1, 0.1f).OnComplete(() => noStamina.DOFade(0, 0.2f));
    }

    public void ChangeCheckpoint(Transform newt)
    {
        if(newt.name == "Checkpoint 1") firstEnemy = true;
        currentCheckPoint = newt;
    }

    private void Respawn(bool heal) {
        ActionCharacter.Instance.dead = false;
        ActionCharacter.Instance.animator.SetBool("dead", false);

        if(heal)
        {
            lifebar.DOFillAmount(1, 0.5f).SetEase(Ease.OutQuad);
            currentLife = totalLife;

            healthCharge = 3;
            
            for (int i = 0; i < 3; i++)
            {
                chargeParent.GetChild(i).gameObject.SetActive(true);
            }

            if(!firstEnemy)
            {
                EnemyManager.activeEnemies[0].Heal();
            }

            int enCount = EnemyManager.activeEnemies.Count;

            for (int i = firstEnemy ? 0 : 1; i < enCount; i++)
            {
                Destroy(EnemyManager.activeEnemies[i].gameObject,0.2f);
            }
        }
    }

    public void SwitchPause()
    {
        isPause = !isPause;

        if(isPause) {
            pauseMenu.SetActive(true);
            hud.SetActive(false);
            cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
            cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
            Time.timeScale = 0;
            defaultButton.Select();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ActionCharacter.Instance.EnableInput(false);
        } else
        {
            pauseMenu.SetActive(false);
            hud.SetActive(true);
            cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = .1f;
            cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = .1f;
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ActionCharacter.Instance.EnableInput(true);
        }
    }
}

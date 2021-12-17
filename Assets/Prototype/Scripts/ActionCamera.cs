using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class ActionCamera : MonoBehaviour
{
    public AnimationCurve zoomCurve;
    public CinemachineVirtualCamera cinemachine;
    public CinemachineStoryboard storyboard;
    private CinemachineFramingTransposer componentBase;
    public Vector3 cameraNearOffset = new Vector3(0.27f, 0, 4.4f);
    public Vector3 cameraFarOffset = new Vector3(0, 0, 6.6f);
    private CinemachineImpulseSource cinemachineNoise;
    private CharacterController characterController;
    private Tween zoomTween;
    private Tween distortion;
    private Sequence zoomOut;
    private bool isInCombat;
    private IEnumerator combatTimer;
    private bool m_isRunning = false;
    private bool isRunning {
        get {return m_isRunning; }
        set
        {
            if(value != m_isRunning)
            {
                m_isRunning = value; 
                ZoomOut(m_isRunning);
            }
        }
    }

    private void Start()
    {
        cinemachineNoise = GetComponent<CinemachineImpulseSource>();
        characterController = ActionCharacter.Instance.characterController;
        componentBase = (CinemachineFramingTransposer)cinemachine.GetCinemachineComponent(CinemachineCore.Stage.Body);

        DOTween.To(() => storyboard.m_Alpha,
            x =>
            {
                storyboard.m_Alpha = x;
            },
            0, 3.4f).SetEase(Ease.Linear);
    }

    private void Update() {
        if(!isInCombat) 
        {
            //Zoom out if speed is high
            var speed = characterController.velocity.magnitude;

            if(!isRunning && speed >= 6)
            {
                isRunning = true;
            } else if (isRunning && speed < 6)
            {
                isRunning = false;
            }
        }
    }

    public void JustDidDamge() {
        isInCombat = true;
        isRunning = true;

        if (combatTimer != null)
            StopCoroutine(combatTimer);

        combatTimer = movementReset();
        StartCoroutine(combatTimer);

        IEnumerator movementReset()
        {
            yield return new WaitForSeconds(4);
            isInCombat = false;
        }
    }

    public void Zoom(float duration, float zoom)
    {
        if (zoomTween != null)
        {
            zoomTween.Kill(true);
        }

        Debug.Log("zoom");

        zoomTween = DOTween.To(() => cinemachine.m_Lens.FieldOfView,
            x =>
            {
                cinemachine.m_Lens.FieldOfView = x;
            },
            zoom, duration).SetEase(zoomCurve);
    }

    public void Shake(float intensity, float duration)
    {
        cinemachineNoise.GenerateImpulse();
    }

    private void ZoomOut(bool isOut)
    {
        if(zoomOut != null)
            zoomOut.Kill(false);

        zoomOut = DOTween.Sequence();

        zoomOut.Append(DOTween.To(() => componentBase.m_CameraDistance,
            x =>
            {
                componentBase.m_CameraDistance = x;
            },
            isOut ? 6.6f : 4f, .9f).SetEase(Ease.InSine));

        zoomOut.Join(DOTween.To(() => componentBase.m_ScreenX,
            y =>
            {
                componentBase.m_ScreenX = y;
            },
            isOut ? 0.5f : 0.4f, .9f).SetEase(Ease.InSine));
        zoomOut.SetDelay(isInCombat ? 0 : 0.8f);
    }
}
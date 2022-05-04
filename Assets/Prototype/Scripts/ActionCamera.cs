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
    public bool isLocked;
    private IEnumerator combatTimer;
    private bool m_isRunning = false;
    private bool isRunning {
        get {return m_isRunning; }
        set
        {
            if(value != m_isRunning)
            {
                m_isRunning = value; 
                if(!isLocked)
                    ZoomOut(m_isRunning);
            }
        }
    }

    private void Start()
    {
        cinemachineNoise = GetComponent<CinemachineImpulseSource>();
        characterController = ActionCharacter.Instance.characterController;
        componentBase = cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>();
        ResetCamera();

        DOTween.To(() => storyboard.m_Alpha,
            x =>
            {
                storyboard.m_Alpha = x;
            },
            0, 3.4f).SetEase(Ease.Linear);
    }

    public void ResetCamera()
    {
        zoomTween.Kill(true);
        zoomOut.Kill(true);
    }

    private void Update() {
        if(!isInCombat && !isLocked) 
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
        if(isLocked) return;

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

    public void Shake(float intensity = 6, float duration = 0.36f)
    {
        cinemachineNoise.m_ImpulseDefinition.m_AmplitudeGain = intensity;
        cinemachineNoise.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = duration;
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
            isOut ? cameraFarOffset.z : cameraNearOffset.z, .9f).SetEase(Ease.InSine));

        zoomOut.Join(DOTween.To(() => componentBase.m_ScreenX,
            y =>
            {
                componentBase.m_ScreenX = y;
            },
            isOut ? cameraFarOffset.x : cameraNearOffset.x, .9f).SetEase(Ease.InSine));
        zoomOut.SetDelay(isInCombat ? 0 : 0.8f);
    }
}

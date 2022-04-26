using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FloatingBox : MonoBehaviour
{
    Sequence seq;
    bool isActive = false;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    GlobalAudioManager audioManager;

    public void Awake()
    {
        audioManager = FindObjectOfType<GlobalAudioManager>();
    }
    public void Popup(string phrase)
    {
        if(seq != null && seq.active) {seq.Kill(true);}
        
        seq = DOTween.Sequence();

        var Box = transform.GetChild(0);
        Box.gameObject.SetActive(true);

        Box.GetComponentInChildren<TextMeshPro>().text = phrase;
        isActive = true;

        seq.Append(Box.DOScale(Vector3.one, 0.4f).From(Vector3.zero).SetEase(Ease.OutBack));
        seq.AppendInterval(3);
        seq.Append(Box.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack));
        seq.OnComplete(() => {Box.gameObject.SetActive(false); isActive = false;});

        switch(Random.Range(1,4))
        {
            case 1:
                audioManager.AudioPlay(clip1, 0.4f, false, transform.position);
                break;
            case 2:
                audioManager.AudioPlay(clip2, 0.4f, false, transform.position);
                transform.parent.GetComponent<Animator>().SetTrigger("talk");
                break;
            case 3:
                audioManager.AudioPlay(clip3, 0.4f, false, transform.position);
                break;
        }
    }

    private void Update() {
        if(isActive)
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Page : MonoBehaviour
{
    void Awake() {
        //Disable All buttons

        var buttons = GetComponentsInChildren<Button>();

        foreach (var button in buttons)
        {
            button.interactable = false;
        }

        //Make Invisible
        var images = GetComponentsInChildren<Image>();
        var texts = GetComponentsInChildren<Text>();
        var tm = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var image in images)
        {
            Color col = image.color;
            col.a = 0;
            image.color = col;
        }


        foreach (var text in texts)
        {
            Color col = text.color;
            col.a = 0;
            text.color = col;
        }

        foreach (var text in tm)
        {
            Color col = text.color;
            col.a = 0;
            text.color = col;
        }
    }

    public void EnterPage()
    {
        //Enable All buttons

        var buttons = GetComponentsInChildren<Button>();

        foreach (var button in buttons)
        {
            button.interactable = true;
        }

        //Fade In
        
        var images = GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            image.DOFade(1, 0.2f).SetUpdate(true);
        }

        var texts = GetComponentsInChildren<Text>();

        foreach (var text in texts)
        {
            text.DOFade(1, 0.2f).SetUpdate(true);
        }

        var tm = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var text in tm)
        {
            text.DOFade(1, 0.2f).SetUpdate(true);
        }
    }

    public void ExitPage()
    {
        //Disable All buttons

        var buttons = GetComponentsInChildren<Button>();

        foreach (var button in buttons)
        {
            button.interactable = false;
        }

        //Fade Out
        
        var images = GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            image.DOFade(0, 0.2f).SetUpdate(true);
        }

        var texts = GetComponentsInChildren<Text>();

        foreach (var text in texts)
        {
            text.DOFade(0, 0.2f).SetUpdate(true);
        }

        var tm = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var text in tm)
        {
            text.DOFade(0, 0.2f).SetUpdate(true);
        }
    }

}

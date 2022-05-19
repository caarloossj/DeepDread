using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsToggle : MonoBehaviour
{
    public string valueIdentifier;
    public string buttonValue;
    public string defaultValue;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color onColor; 
    [SerializeField] private Color pressedColor; 

    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
        ResetColor();
    }

    public void ChangeValue()
    {
        PlayerPrefs.SetString(valueIdentifier, buttonValue);

        if(valueIdentifier == "resolution")
        {
            string[] res = buttonValue.Split('x');
            Screen.SetResolution(Int32.Parse(res[0]), Int32.Parse(res[1]), true);
        }
        else
        {
            switch(buttonValue)
            {
                case "low":
                    QualitySettings.SetQualityLevel(0, true);
                    break;
                case "default":
                    QualitySettings.SetQualityLevel(1, true);
                    break;
                case "high":
                    QualitySettings.SetQualityLevel(2, true);
                    break;
                case "ultra":
                    QualitySettings.SetQualityLevel(2, true);
                    break;
            }
        }

        foreach (SettingsToggle button in FindObjectsOfType<SettingsToggle>())
        {
            button.ResetColor();
        }
    }

    public void ResetColor()
    {
        string currentValue = PlayerPrefs.GetString(valueIdentifier, defaultValue);

        var colors = button.colors;
        if(buttonValue == currentValue)
        {
            colors.normalColor = onColor;
            colors.selectedColor = onColor;
        } else 
        {
            colors.normalColor = normalColor;
            colors.selectedColor = pressedColor;
        }

        button.colors = colors;
    }
}

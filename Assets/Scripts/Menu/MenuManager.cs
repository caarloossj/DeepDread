using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public string currentMenu = "main";
    public Animator canvasAnimator;
    public CinemachineVirtualCamera farCamera;
    public CinemachineVirtualCamera robotCamera;
    public CinemachineVirtualCamera playCamera;
    public Dropdown qualitySettings;
    public Dropdown resolutionSettings;

    #region Singleton
   //Singleton
    public static MenuManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(MenuManager)) as MenuManager;
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    private static MenuManager _instance;
    #endregion

    public void SwitchMenu (string menu)
    {
        if(currentMenu == menu)
            return;

        currentMenu = menu;
        
        switch(currentMenu){
            case "main":
                farCamera.m_Priority = 1;
                robotCamera.m_Priority = 0;
                playCamera.m_Priority = 0;
                canvasAnimator.SetTrigger("main");
                break;
            case "options":
                farCamera.m_Priority = 0;
                robotCamera.m_Priority = 1;
                playCamera.m_Priority = 0;
                canvasAnimator.SetTrigger("options");
                break;
            case "play":
                farCamera.m_Priority = 0;
                robotCamera.m_Priority = 0;
                playCamera.m_Priority = 1;
                canvasAnimator.SetTrigger("play");
                break;
        }
    }

    public void ChangeQuality() {
        QualitySettings.SetQualityLevel(qualitySettings.value, true);
    }

    public void ChangeResolution() {
        
        Vector2Int res = new Vector2Int(1280, 720);

        switch(resolutionSettings.value)
        {
            case 0:
                res = new Vector2Int(1280, 720);
                break;
            case 1:
                res = new Vector2Int(1600, 1200);
                break;
            case 2:
                res = new Vector2Int(1920, 1080);
                break;
        }
        Screen.SetResolution(res.x, res.y, FullScreenMode.ExclusiveFullScreen);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //[SerializeField] private Page startingPage;
    private Page currentPage;
    public Button lastButton;
    private bool inOptions;
    public bool isOnValue = false;
    public AudioClip valueSound;
    public AudioSource generalSource;
    public PlayableDirector playTimeline;

    private UIInputHandler inputHandler;

    void Start()
    {
        inputHandler = UIInputHandler.instance;

        //inputHandler.UIControls.UI.TextSkip.performed += context => Return();

        inputHandler.UIControls.UI.Return.performed += context => Return();
        inputHandler.UIControls.UI.RightValue.performed += context => ControllerValueInput(true);
        inputHandler.UIControls.UI.LeftValue.performed += context => ControllerValueInput(false);
    }

    private void ControllerValueInput(bool increase)
    {
        if(isOnValue)
        {
            SettingsValue setting = EventSystem.current.currentSelectedGameObject.GetComponent<SettingsValue>();
            ModifyValue(setting, increase);
        }
    }

    public void ModifyValue(SettingsValue setting, bool increase)
    {
        //If a settings value is selected, change the value
        
        generalSource.PlayOneShot(valueSound);

        int newValue = PlayerPrefs.GetInt(setting.valueIdentifier, setting.defaultValue) + (increase ? 1 : -1);
        newValue = Mathf.Clamp(newValue, 0, 10);
        PlayerPrefs.SetInt(setting.valueIdentifier, newValue);

        setting.SetScale(newValue);
    }

    private void Return()
    {
        //If in options, return to select last button
        inOptions = EventSystem.current.currentSelectedGameObject != lastButton.gameObject;

        if(inOptions)
        {
            lastButton.Select();
        }
    }

    public void Play() {
        playTimeline.Play();

        StartCoroutine(EnterScene());

        IEnumerator EnterScene() {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Level_1");
        }
    }

    public void ChangePage(Page newPage)
    {
        //Disable previous page
        if(currentPage != null && currentPage != newPage)
        {
            currentPage.ExitPage();
        }

        if(currentPage != newPage)
            //Enable the new page
            newPage.EnterPage();

        currentPage = newPage;
    }
}

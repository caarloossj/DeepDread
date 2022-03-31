using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsValue : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public string valueIdentifier;
    public Transform barTransform;
    private Vector3 initialSize;
    public int defaultValue;

    public void Awake()
    {
        //Get the initial size
        initialSize = barTransform.localScale;
        SetScale(PlayerPrefs.GetInt(valueIdentifier, defaultValue));
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Set the modified value string
        FindObjectOfType<MenuManager>().isOnValue = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //Remove the modified value string
        FindObjectOfType<MenuManager>().isOnValue = false;
    }

    public void SetScale(int value)
    {
        Vector3 newScale = initialSize;
        newScale.x = (initialSize.x/11) * (value);

        barTransform.localScale = newScale;
    }

    public void ModifyValue(bool increase)
    {
        FindObjectOfType<MenuManager>().ModifyValue(this, increase);
    }
}

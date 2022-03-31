using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    public static UIInputHandler instance;
    public UIControl UIControls;

    private void Awake() {
        if(instance == null)
            instance = this;
        else 
            Destroy(gameObject);

        UIControls = new UIControl();
    }

    private void OnEnable()
    {
        UIControls.UI.Enable();
    }
    private void OnDisable()
    {    
        UIControls.UI.Disable();
    }
}

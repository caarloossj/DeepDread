// GENERATED AUTOMATICALLY FROM 'Assets/Input/UIControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @UIControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @UIControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIControl"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""6739e0f0-4c3f-4e79-ab04-46257a6a5419"",
            ""actions"": [
                {
                    ""name"": ""TextSkip"",
                    ""type"": ""Button"",
                    ""id"": ""1b544958-6ca4-42ea-ad56-e26050e1b7f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Return"",
                    ""type"": ""Button"",
                    ""id"": ""5e9b3b2a-1f36-4bf6-997c-1cfacb2c35fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c05393d7-478e-4221-af0f-985880339ebc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TextSkip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de014589-a770-4d6a-a2a4-511bd04aa589"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TextSkip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bceca12c-b716-41ed-a4cb-39b5479c6a89"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TextSkip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9762ff75-6c6f-4adb-92fd-7c1f2510a9a9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_TextSkip = m_UI.FindAction("TextSkip", throwIfNotFound: true);
        m_UI_Return = m_UI.FindAction("Return", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_TextSkip;
    private readonly InputAction m_UI_Return;
    public struct UIActions
    {
        private @UIControl m_Wrapper;
        public UIActions(@UIControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @TextSkip => m_Wrapper.m_UI_TextSkip;
        public InputAction @Return => m_Wrapper.m_UI_Return;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @TextSkip.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTextSkip;
                @TextSkip.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTextSkip;
                @TextSkip.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTextSkip;
                @Return.started -= m_Wrapper.m_UIActionsCallbackInterface.OnReturn;
                @Return.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnReturn;
                @Return.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnReturn;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TextSkip.started += instance.OnTextSkip;
                @TextSkip.performed += instance.OnTextSkip;
                @TextSkip.canceled += instance.OnTextSkip;
                @Return.started += instance.OnReturn;
                @Return.performed += instance.OnReturn;
                @Return.canceled += instance.OnReturn;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IUIActions
    {
        void OnTextSkip(InputAction.CallbackContext context);
        void OnReturn(InputAction.CallbackContext context);
    }
}

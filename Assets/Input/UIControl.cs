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
                },
                {
                    ""name"": ""RightValue"",
                    ""type"": ""Button"",
                    ""id"": ""7b4b78ef-2ec5-49b8-ab2f-009ec746bff4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftValue"",
                    ""type"": ""Button"",
                    ""id"": ""48e9f54c-7abe-452c-befa-32a9eb8fa817"",
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
                    ""id"": ""d1f9f0db-34d8-4656-8f2b-44cbc1e55329"",
                    ""path"": ""<Gamepad>/buttonSouth"",
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
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20496e62-3576-43ba-9c27-e45e0c6c2eac"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""850c08c0-0023-4ea7-a74d-19fa540e4114"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightValue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e17aa74e-2760-4b85-a6a5-5b8a97657ce0"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightValue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3a32c7e-793e-4a16-81a2-43911930ae1b"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightValue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e1682e7-dfa8-429e-b6d2-a7f7952aae0f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftValue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e1e346d-72fd-498f-8b91-7f2d04253c55"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftValue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35ebd10d-07c9-4a1f-8f5e-530bda0d941c"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftValue"",
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
        m_UI_RightValue = m_UI.FindAction("RightValue", throwIfNotFound: true);
        m_UI_LeftValue = m_UI.FindAction("LeftValue", throwIfNotFound: true);
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
    private readonly InputAction m_UI_RightValue;
    private readonly InputAction m_UI_LeftValue;
    public struct UIActions
    {
        private @UIControl m_Wrapper;
        public UIActions(@UIControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @TextSkip => m_Wrapper.m_UI_TextSkip;
        public InputAction @Return => m_Wrapper.m_UI_Return;
        public InputAction @RightValue => m_Wrapper.m_UI_RightValue;
        public InputAction @LeftValue => m_Wrapper.m_UI_LeftValue;
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
                @RightValue.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRightValue;
                @RightValue.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRightValue;
                @RightValue.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRightValue;
                @LeftValue.started -= m_Wrapper.m_UIActionsCallbackInterface.OnLeftValue;
                @LeftValue.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnLeftValue;
                @LeftValue.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnLeftValue;
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
                @RightValue.started += instance.OnRightValue;
                @RightValue.performed += instance.OnRightValue;
                @RightValue.canceled += instance.OnRightValue;
                @LeftValue.started += instance.OnLeftValue;
                @LeftValue.performed += instance.OnLeftValue;
                @LeftValue.canceled += instance.OnLeftValue;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IUIActions
    {
        void OnTextSkip(InputAction.CallbackContext context);
        void OnReturn(InputAction.CallbackContext context);
        void OnRightValue(InputAction.CallbackContext context);
        void OnLeftValue(InputAction.CallbackContext context);
    }
}

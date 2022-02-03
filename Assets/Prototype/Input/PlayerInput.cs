// GENERATED AUTOMATICALLY FROM 'Assets/Prototype/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""667e43aa-9f83-4441-a2a2-84f442270f58"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""64a390f9-305f-46b2-b0e6-74ed67eb9669"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""9facb13a-27e7-4432-a5e8-3c7c2380b555"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""b95d3651-55af-4e55-bc3d-6d8a9a8aeb98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraControl"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c034af42-2ee9-4d3f-aeab-39a666e91af9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e31f537d-43fc-49f7-bfac-d7d09d9647fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""c20fe61c-42cf-44a5-848c-ee3fc87b10b3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchGod"",
                    ""type"": ""Button"",
                    ""id"": ""4af14f3f-0237-4129-96ed-cac5bf49475f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GoToLevel1"",
                    ""type"": ""Button"",
                    ""id"": ""cffcf6ca-6a66-43a3-8c7f-b529edab5f41"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GoToLevel2"",
                    ""type"": ""Button"",
                    ""id"": ""dbc149cd-29b5-4352-b819-1bb8feadeae1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GoToLevel3"",
                    ""type"": ""Button"",
                    ""id"": ""3d7200ba-04b9-413c-a0e3-7059e2370f0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TargetLock"",
                    ""type"": ""Button"",
                    ""id"": ""88c1b0ef-a7af-402f-81e8-65f202b97e54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f5443536-a4d5-4dd4-be1b-60f31bbb634b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""05ae51b1-3a37-4ec0-b353-b96255088a45"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9bd1c153-be1a-41f4-9471-2bae1650958d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9282f8d5-07f7-4ca8-9c74-deb7b027b8e4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6d8f6525-f190-4910-98db-d727b531d405"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0bf6253c-f4e0-4e03-ba6d-61021fe89c17"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""50a6029d-85d9-4945-8ca5-411d504c9991"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5410149-9e4a-42f4-b479-494735a3b789"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0398490b-0a01-4fb5-b230-e02aa1e5d5cb"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a4ba5d8-b442-4906-a8e8-1c97b17834d9"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc054a40-4697-4bea-af45-232048d9b40e"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.3),NormalizeVector2,ScaleVector2(x=28,y=20)"",
                    ""groups"": """",
                    ""action"": ""CameraControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""797049b6-16ad-4a2d-80ed-fe2b22fdca67"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43e6479d-998f-4281-bdab-fd193eff7a6f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bdfce5aa-3482-4ebe-b855-4b7385e1b353"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""507f6003-7efb-42e5-8cbf-3766cb245787"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4ef9647-810a-42cd-9a1a-e3157af1811c"",
                    ""path"": ""<Keyboard>/f10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchGod"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dfd0276d-091d-4e85-ac42-152b21b29a5f"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoToLevel1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0c4b173-de2e-4bcf-874f-a00d9e790063"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoToLevel2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5820323-b3eb-47be-9ff5-61bd6628530d"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoToLevel3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9415097f-9580-4a12-86dd-904585cca3db"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TargetLock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UIControls"",
            ""id"": ""66f47ac6-f9df-47e2-a43d-d8eeda4c84d7"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""477a035d-8312-4a18-b10b-839b5b27042f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""64945718-9db4-4069-ab2a-7b83c06cd8f6"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterControls
        m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
        m_CharacterControls_Move = m_CharacterControls.FindAction("Move", throwIfNotFound: true);
        m_CharacterControls_Run = m_CharacterControls.FindAction("Run", throwIfNotFound: true);
        m_CharacterControls_Roll = m_CharacterControls.FindAction("Roll", throwIfNotFound: true);
        m_CharacterControls_CameraControl = m_CharacterControls.FindAction("CameraControl", throwIfNotFound: true);
        m_CharacterControls_Jump = m_CharacterControls.FindAction("Jump", throwIfNotFound: true);
        m_CharacterControls_Attack = m_CharacterControls.FindAction("Attack", throwIfNotFound: true);
        m_CharacterControls_SwitchGod = m_CharacterControls.FindAction("SwitchGod", throwIfNotFound: true);
        m_CharacterControls_GoToLevel1 = m_CharacterControls.FindAction("GoToLevel1", throwIfNotFound: true);
        m_CharacterControls_GoToLevel2 = m_CharacterControls.FindAction("GoToLevel2", throwIfNotFound: true);
        m_CharacterControls_GoToLevel3 = m_CharacterControls.FindAction("GoToLevel3", throwIfNotFound: true);
        m_CharacterControls_TargetLock = m_CharacterControls.FindAction("TargetLock", throwIfNotFound: true);
        // UIControls
        m_UIControls = asset.FindActionMap("UIControls", throwIfNotFound: true);
        m_UIControls_Pause = m_UIControls.FindAction("Pause", throwIfNotFound: true);
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

    // CharacterControls
    private readonly InputActionMap m_CharacterControls;
    private ICharacterControlsActions m_CharacterControlsActionsCallbackInterface;
    private readonly InputAction m_CharacterControls_Move;
    private readonly InputAction m_CharacterControls_Run;
    private readonly InputAction m_CharacterControls_Roll;
    private readonly InputAction m_CharacterControls_CameraControl;
    private readonly InputAction m_CharacterControls_Jump;
    private readonly InputAction m_CharacterControls_Attack;
    private readonly InputAction m_CharacterControls_SwitchGod;
    private readonly InputAction m_CharacterControls_GoToLevel1;
    private readonly InputAction m_CharacterControls_GoToLevel2;
    private readonly InputAction m_CharacterControls_GoToLevel3;
    private readonly InputAction m_CharacterControls_TargetLock;
    public struct CharacterControlsActions
    {
        private @PlayerInput m_Wrapper;
        public CharacterControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterControls_Move;
        public InputAction @Run => m_Wrapper.m_CharacterControls_Run;
        public InputAction @Roll => m_Wrapper.m_CharacterControls_Roll;
        public InputAction @CameraControl => m_Wrapper.m_CharacterControls_CameraControl;
        public InputAction @Jump => m_Wrapper.m_CharacterControls_Jump;
        public InputAction @Attack => m_Wrapper.m_CharacterControls_Attack;
        public InputAction @SwitchGod => m_Wrapper.m_CharacterControls_SwitchGod;
        public InputAction @GoToLevel1 => m_Wrapper.m_CharacterControls_GoToLevel1;
        public InputAction @GoToLevel2 => m_Wrapper.m_CharacterControls_GoToLevel2;
        public InputAction @GoToLevel3 => m_Wrapper.m_CharacterControls_GoToLevel3;
        public InputAction @TargetLock => m_Wrapper.m_CharacterControls_TargetLock;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControlsActions instance)
        {
            if (m_Wrapper.m_CharacterControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Roll.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRoll;
                @CameraControl.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnCameraControl;
                @CameraControl.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnCameraControl;
                @CameraControl.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnCameraControl;
                @Jump.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAttack;
                @SwitchGod.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSwitchGod;
                @SwitchGod.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSwitchGod;
                @SwitchGod.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSwitchGod;
                @GoToLevel1.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnGoToLevel1;
                @GoToLevel1.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnGoToLevel1;
                @GoToLevel1.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnGoToLevel1;
                @GoToLevel2.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnGoToLevel2;
                @GoToLevel2.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnGoToLevel2;
                @GoToLevel2.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnGoToLevel2;
                @GoToLevel3.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnGoToLevel3;
                @GoToLevel3.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnGoToLevel3;
                @GoToLevel3.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnGoToLevel3;
                @TargetLock.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnTargetLock;
                @TargetLock.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnTargetLock;
                @TargetLock.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnTargetLock;
            }
            m_Wrapper.m_CharacterControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @CameraControl.started += instance.OnCameraControl;
                @CameraControl.performed += instance.OnCameraControl;
                @CameraControl.canceled += instance.OnCameraControl;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @SwitchGod.started += instance.OnSwitchGod;
                @SwitchGod.performed += instance.OnSwitchGod;
                @SwitchGod.canceled += instance.OnSwitchGod;
                @GoToLevel1.started += instance.OnGoToLevel1;
                @GoToLevel1.performed += instance.OnGoToLevel1;
                @GoToLevel1.canceled += instance.OnGoToLevel1;
                @GoToLevel2.started += instance.OnGoToLevel2;
                @GoToLevel2.performed += instance.OnGoToLevel2;
                @GoToLevel2.canceled += instance.OnGoToLevel2;
                @GoToLevel3.started += instance.OnGoToLevel3;
                @GoToLevel3.performed += instance.OnGoToLevel3;
                @GoToLevel3.canceled += instance.OnGoToLevel3;
                @TargetLock.started += instance.OnTargetLock;
                @TargetLock.performed += instance.OnTargetLock;
                @TargetLock.canceled += instance.OnTargetLock;
            }
        }
    }
    public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);

    // UIControls
    private readonly InputActionMap m_UIControls;
    private IUIControlsActions m_UIControlsActionsCallbackInterface;
    private readonly InputAction m_UIControls_Pause;
    public struct UIControlsActions
    {
        private @PlayerInput m_Wrapper;
        public UIControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_UIControls_Pause;
        public InputActionMap Get() { return m_Wrapper.m_UIControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIControlsActions set) { return set.Get(); }
        public void SetCallbacks(IUIControlsActions instance)
        {
            if (m_Wrapper.m_UIControlsActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_UIControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public UIControlsActions @UIControls => new UIControlsActions(this);
    public interface ICharacterControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnCameraControl(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnSwitchGod(InputAction.CallbackContext context);
        void OnGoToLevel1(InputAction.CallbackContext context);
        void OnGoToLevel2(InputAction.CallbackContext context);
        void OnGoToLevel3(InputAction.CallbackContext context);
        void OnTargetLock(InputAction.CallbackContext context);
    }
    public interface IUIControlsActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}

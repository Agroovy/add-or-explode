// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerInput.inputactions'

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
            ""name"": ""All"",
            ""id"": ""f2bf868c-5440-4c62-b5ab-49021cd99e7b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""25b01d80-9bb8-4e36-8e66-d2e727685121"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""454a2986-5ec8-42fd-ade6-ac4a8ffbaab7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2ce4959c-15e4-4a84-9bc2-2e5a66d63b8c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fullscreen"",
                    ""type"": ""Button"",
                    ""id"": ""84efdeae-d302-4009-85c5-3aab31a4885d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""f678edd4-7486-4a2a-b499-1ef4aa8fd278"",
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
                    ""id"": ""536835db-834e-4ece-a298-5d59a03d11f5"",
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
                    ""id"": ""aed91c23-a838-4e5b-8f5c-e06cb34327df"",
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
                    ""id"": ""19d811be-d529-4f03-a403-30fb51cb49a5"",
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
                    ""id"": ""3410d47e-8c21-4108-8939-684228b01b9c"",
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
                    ""id"": ""45ca2651-ab36-4011-aa61-a55957f0fa2a"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""2ff21e3c-6668-40c0-b725-3ea7dd9e471b"",
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
                    ""id"": ""692b764d-2a8a-4b7c-96e0-593685d31dca"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e30a6dc9-617d-4e43-a733-02b64edede51"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d21c2331-583f-4050-b3a0-30442e3da9a4"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9ca6f43b-c15d-4a83-8a60-d6014b6267db"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9ba43f11-6c63-4221-ac3e-5010bd3e947e"",
                    ""path"": ""*/{Menu}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""297abde5-f792-4a24-9dcf-d700465322a0"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23b575b4-2583-44a8-bffb-8186c7d849b0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""332d30bd-e837-4b4a-a867-a310c9da6e82"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f96015b5-279f-4080-b6d0-9a5fa634725d"",
                    ""path"": ""<Keyboard>/f11"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fullscreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // All
        m_All = asset.FindActionMap("All", throwIfNotFound: true);
        m_All_Move = m_All.FindAction("Move", throwIfNotFound: true);
        m_All_Pause = m_All.FindAction("Pause", throwIfNotFound: true);
        m_All_Submit = m_All.FindAction("Submit", throwIfNotFound: true);
        m_All_Fullscreen = m_All.FindAction("Fullscreen", throwIfNotFound: true);
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

    // All
    private readonly InputActionMap m_All;
    private IAllActions m_AllActionsCallbackInterface;
    private readonly InputAction m_All_Move;
    private readonly InputAction m_All_Pause;
    private readonly InputAction m_All_Submit;
    private readonly InputAction m_All_Fullscreen;
    public struct AllActions
    {
        private @PlayerInput m_Wrapper;
        public AllActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_All_Move;
        public InputAction @Pause => m_Wrapper.m_All_Pause;
        public InputAction @Submit => m_Wrapper.m_All_Submit;
        public InputAction @Fullscreen => m_Wrapper.m_All_Fullscreen;
        public InputActionMap Get() { return m_Wrapper.m_All; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AllActions set) { return set.Get(); }
        public void SetCallbacks(IAllActions instance)
        {
            if (m_Wrapper.m_AllActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_AllActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_AllActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_AllActionsCallbackInterface.OnMove;
                @Pause.started -= m_Wrapper.m_AllActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_AllActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_AllActionsCallbackInterface.OnPause;
                @Submit.started -= m_Wrapper.m_AllActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_AllActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_AllActionsCallbackInterface.OnSubmit;
                @Fullscreen.started -= m_Wrapper.m_AllActionsCallbackInterface.OnFullscreen;
                @Fullscreen.performed -= m_Wrapper.m_AllActionsCallbackInterface.OnFullscreen;
                @Fullscreen.canceled -= m_Wrapper.m_AllActionsCallbackInterface.OnFullscreen;
            }
            m_Wrapper.m_AllActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Fullscreen.started += instance.OnFullscreen;
                @Fullscreen.performed += instance.OnFullscreen;
                @Fullscreen.canceled += instance.OnFullscreen;
            }
        }
    }
    public AllActions @All => new AllActions(this);
    public interface IAllActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnFullscreen(InputAction.CallbackContext context);
    }
}

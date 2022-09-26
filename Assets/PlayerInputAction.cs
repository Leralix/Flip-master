//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/PlayerInputAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""PlayerAZERTY"",
            ""id"": ""fa77d8ca-21d6-4ce7-a864-bd07dfdc91ff"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""cfe2b543-2dbe-4927-9ca3-dfd320dbbd92"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""ef0bb791-7ba9-47c4-b4a3-5a5d1ce0e756"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""cbe1f11e-0c0a-4b76-90bb-fc69b83aca0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""b968c549-527a-4737-8dfe-9db7dc6e5b19"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ed62cecc-7df0-4920-8767-66a0080f0b26"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""484fc704-5bba-48c3-86ab-b270202e724f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0313f9bf-0325-4294-8439-04a793dfce25"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8c0e54e5-e29d-45e3-a490-49b047085659"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c6119713-3918-4717-8153-3491548b49ba"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67987c35-15e5-4a48-bc65-799bb7de2960"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerAZERTY
        m_PlayerAZERTY = asset.FindActionMap("PlayerAZERTY", throwIfNotFound: true);
        m_PlayerAZERTY_Move = m_PlayerAZERTY.FindAction("Move", throwIfNotFound: true);
        m_PlayerAZERTY_Jump = m_PlayerAZERTY.FindAction("Jump", throwIfNotFound: true);
        m_PlayerAZERTY_Shoot = m_PlayerAZERTY.FindAction("Shoot", throwIfNotFound: true);
        m_PlayerAZERTY_Interact = m_PlayerAZERTY.FindAction("Interact", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerAZERTY
    private readonly InputActionMap m_PlayerAZERTY;
    private IPlayerAZERTYActions m_PlayerAZERTYActionsCallbackInterface;
    private readonly InputAction m_PlayerAZERTY_Move;
    private readonly InputAction m_PlayerAZERTY_Jump;
    private readonly InputAction m_PlayerAZERTY_Shoot;
    private readonly InputAction m_PlayerAZERTY_Interact;
    public struct PlayerAZERTYActions
    {
        private @PlayerInputAction m_Wrapper;
        public PlayerAZERTYActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerAZERTY_Move;
        public InputAction @Jump => m_Wrapper.m_PlayerAZERTY_Jump;
        public InputAction @Shoot => m_Wrapper.m_PlayerAZERTY_Shoot;
        public InputAction @Interact => m_Wrapper.m_PlayerAZERTY_Interact;
        public InputActionMap Get() { return m_Wrapper.m_PlayerAZERTY; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerAZERTYActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerAZERTYActions instance)
        {
            if (m_Wrapper.m_PlayerAZERTYActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnJump;
                @Shoot.started -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnShoot;
                @Interact.started -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerAZERTYActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_PlayerAZERTYActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public PlayerAZERTYActions @PlayerAZERTY => new PlayerAZERTYActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IPlayerAZERTYActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}

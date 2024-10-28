//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/InputSystem/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player_Grounded"",
            ""id"": ""6be48e02-2d05-4b44-903a-ccd546555b0b"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e5b0c29d-9669-4497-b4bf-c4af1361b58a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""474a084f-9f1f-433c-80c6-3add590e6161"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ShootHooks"",
                    ""type"": ""Button"",
                    ""id"": ""eae3420f-5d20-4142-a9b4-927e2fc5a482"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73a9dab0-52e8-4fb6-b362-6484afead3f7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""493a662c-3cc7-4604-ae63-d47b468d64a5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f2b88111-c347-492c-9510-5dbf01d5622b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""de1ed8c5-aff8-4e34-a66f-a0108336e341"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cee3482d-5747-4f4f-99b6-7f2654c81cba"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f02bff76-8671-47d8-ae28-0f4b7181b30e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""da2215e8-8085-4e2d-a6c8-b757c473016b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7bc00cbb-6477-423d-b5c8-99fcca3c18f2"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20810585-f003-4269-b954-f07a3e803154"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ShootHooks"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player_MidAir"",
            ""id"": ""983c6d87-ef63-4fd3-af65-298cc38ec07b"",
            ""actions"": [
                {
                    ""name"": ""ShootHooks"",
                    ""type"": ""Button"",
                    ""id"": ""d8eca66e-1041-4109-b773-7575925ab578"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6fa55d5c-5f73-4f4b-baa8-3f6919ba348d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootHooks"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player_Hooked"",
            ""id"": ""b2c38c4a-a397-46d5-b17c-966fd2b034b5"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""8dbcc175-ddc4-4a2c-b643-0434ab1dde3a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6b9f6335-578f-4ae8-a8e5-9cfc94c67f33"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""495df286-b27a-4b16-80e3-b7b22c985e75"",
            ""actions"": [
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""a54088c8-d75d-4791-a96d-fd52aa578188"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ec0fe546-abf8-452b-8681-a820a3f64f4f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
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
        // Player_Grounded
        m_Player_Grounded = asset.FindActionMap("Player_Grounded", throwIfNotFound: true);
        m_Player_Grounded_Jump = m_Player_Grounded.FindAction("Jump", throwIfNotFound: true);
        m_Player_Grounded_Movement = m_Player_Grounded.FindAction("Movement", throwIfNotFound: true);
        m_Player_Grounded_ShootHooks = m_Player_Grounded.FindAction("ShootHooks", throwIfNotFound: true);
        // Player_MidAir
        m_Player_MidAir = asset.FindActionMap("Player_MidAir", throwIfNotFound: true);
        m_Player_MidAir_ShootHooks = m_Player_MidAir.FindAction("ShootHooks", throwIfNotFound: true);
        // Player_Hooked
        m_Player_Hooked = asset.FindActionMap("Player_Hooked", throwIfNotFound: true);
        m_Player_Hooked_Newaction = m_Player_Hooked.FindAction("New action", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
    }

    ~@PlayerInputActions()
    {
        UnityEngine.Debug.Assert(!m_Player_Grounded.enabled, "This will cause a leak and performance issues, PlayerInputActions.Player_Grounded.Disable() has not been called.");
        UnityEngine.Debug.Assert(!m_Player_MidAir.enabled, "This will cause a leak and performance issues, PlayerInputActions.Player_MidAir.Disable() has not been called.");
        UnityEngine.Debug.Assert(!m_Player_Hooked.enabled, "This will cause a leak and performance issues, PlayerInputActions.Player_Hooked.Disable() has not been called.");
        UnityEngine.Debug.Assert(!m_UI.enabled, "This will cause a leak and performance issues, PlayerInputActions.UI.Disable() has not been called.");
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

    // Player_Grounded
    private readonly InputActionMap m_Player_Grounded;
    private List<IPlayer_GroundedActions> m_Player_GroundedActionsCallbackInterfaces = new List<IPlayer_GroundedActions>();
    private readonly InputAction m_Player_Grounded_Jump;
    private readonly InputAction m_Player_Grounded_Movement;
    private readonly InputAction m_Player_Grounded_ShootHooks;
    public struct Player_GroundedActions
    {
        private @PlayerInputActions m_Wrapper;
        public Player_GroundedActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Player_Grounded_Jump;
        public InputAction @Movement => m_Wrapper.m_Player_Grounded_Movement;
        public InputAction @ShootHooks => m_Wrapper.m_Player_Grounded_ShootHooks;
        public InputActionMap Get() { return m_Wrapper.m_Player_Grounded; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_GroundedActions set) { return set.Get(); }
        public void AddCallbacks(IPlayer_GroundedActions instance)
        {
            if (instance == null || m_Wrapper.m_Player_GroundedActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player_GroundedActionsCallbackInterfaces.Add(instance);
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @ShootHooks.started += instance.OnShootHooks;
            @ShootHooks.performed += instance.OnShootHooks;
            @ShootHooks.canceled += instance.OnShootHooks;
        }

        private void UnregisterCallbacks(IPlayer_GroundedActions instance)
        {
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @ShootHooks.started -= instance.OnShootHooks;
            @ShootHooks.performed -= instance.OnShootHooks;
            @ShootHooks.canceled -= instance.OnShootHooks;
        }

        public void RemoveCallbacks(IPlayer_GroundedActions instance)
        {
            if (m_Wrapper.m_Player_GroundedActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer_GroundedActions instance)
        {
            foreach (var item in m_Wrapper.m_Player_GroundedActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player_GroundedActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player_GroundedActions @Player_Grounded => new Player_GroundedActions(this);

    // Player_MidAir
    private readonly InputActionMap m_Player_MidAir;
    private List<IPlayer_MidAirActions> m_Player_MidAirActionsCallbackInterfaces = new List<IPlayer_MidAirActions>();
    private readonly InputAction m_Player_MidAir_ShootHooks;
    public struct Player_MidAirActions
    {
        private @PlayerInputActions m_Wrapper;
        public Player_MidAirActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ShootHooks => m_Wrapper.m_Player_MidAir_ShootHooks;
        public InputActionMap Get() { return m_Wrapper.m_Player_MidAir; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_MidAirActions set) { return set.Get(); }
        public void AddCallbacks(IPlayer_MidAirActions instance)
        {
            if (instance == null || m_Wrapper.m_Player_MidAirActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player_MidAirActionsCallbackInterfaces.Add(instance);
            @ShootHooks.started += instance.OnShootHooks;
            @ShootHooks.performed += instance.OnShootHooks;
            @ShootHooks.canceled += instance.OnShootHooks;
        }

        private void UnregisterCallbacks(IPlayer_MidAirActions instance)
        {
            @ShootHooks.started -= instance.OnShootHooks;
            @ShootHooks.performed -= instance.OnShootHooks;
            @ShootHooks.canceled -= instance.OnShootHooks;
        }

        public void RemoveCallbacks(IPlayer_MidAirActions instance)
        {
            if (m_Wrapper.m_Player_MidAirActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer_MidAirActions instance)
        {
            foreach (var item in m_Wrapper.m_Player_MidAirActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player_MidAirActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player_MidAirActions @Player_MidAir => new Player_MidAirActions(this);

    // Player_Hooked
    private readonly InputActionMap m_Player_Hooked;
    private List<IPlayer_HookedActions> m_Player_HookedActionsCallbackInterfaces = new List<IPlayer_HookedActions>();
    private readonly InputAction m_Player_Hooked_Newaction;
    public struct Player_HookedActions
    {
        private @PlayerInputActions m_Wrapper;
        public Player_HookedActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Player_Hooked_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Player_Hooked; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_HookedActions set) { return set.Get(); }
        public void AddCallbacks(IPlayer_HookedActions instance)
        {
            if (instance == null || m_Wrapper.m_Player_HookedActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player_HookedActionsCallbackInterfaces.Add(instance);
            @Newaction.started += instance.OnNewaction;
            @Newaction.performed += instance.OnNewaction;
            @Newaction.canceled += instance.OnNewaction;
        }

        private void UnregisterCallbacks(IPlayer_HookedActions instance)
        {
            @Newaction.started -= instance.OnNewaction;
            @Newaction.performed -= instance.OnNewaction;
            @Newaction.canceled -= instance.OnNewaction;
        }

        public void RemoveCallbacks(IPlayer_HookedActions instance)
        {
            if (m_Wrapper.m_Player_HookedActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer_HookedActions instance)
        {
            foreach (var item in m_Wrapper.m_Player_HookedActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player_HookedActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player_HookedActions @Player_Hooked => new Player_HookedActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_Submit;
    public struct UIActions
    {
        private @PlayerInputActions m_Wrapper;
        public UIActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @Submit.started += instance.OnSubmit;
            @Submit.performed += instance.OnSubmit;
            @Submit.canceled += instance.OnSubmit;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @Submit.started -= instance.OnSubmit;
            @Submit.performed -= instance.OnSubmit;
            @Submit.canceled -= instance.OnSubmit;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IPlayer_GroundedActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnShootHooks(InputAction.CallbackContext context);
    }
    public interface IPlayer_MidAirActions
    {
        void OnShootHooks(InputAction.CallbackContext context);
    }
    public interface IPlayer_HookedActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnSubmit(InputAction.CallbackContext context);
    }
}

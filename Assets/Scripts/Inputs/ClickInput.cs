using UnityEngine;
using UnityEngine.InputSystem;

// This is the implementation for a specific game input type: Click Input.
sealed class ClickInput : GameInput
{
    // This is a singleton pattern implementation to ensure only one instance of the class is created.
    private static ClickInput instance;
    public static ClickInput Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ClickInput>();
            }
            return instance;
        }
    }

    // This method returns the current input status (i.e. if the player has clicked or tapped).
    public override bool GetInputStatus()
    {
        InputStatus = PlayerControls.PlayerActionMap.PlayerMove.triggered;
        return InputStatus;
    }

    // This method return the input position (i.e. where the player has clicked or tapped).
    public override Vector3 GetInputPosition()
    {
        if (GetInputStatus())
        {
            GetRay();
            RaycastHit hit;

            // Check if the raycast hit an object with the "Ground" tag and set the input position accordingly.
            if (Physics.Raycast(CurrentRay, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    InputPosition = new Vector3(hit.point.x, 0, hit.point.z);
                }
                else
                {
                    InputPosition = Vector3.zero;
                }
            }
        }
        return InputPosition;
    }

    // This method gets the ray for the current input type (i.e. mouse or touch).
    public override Ray GetRay()
    {
#if UNITY_STANDALONE                               
        CurrentRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
#elif UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        CurrentRay = Camera.main.ScreenPointToRay(Touchscreen.current.position.ReadValue());
#endif
        return CurrentRay;
    }

    // This methord return the last raycast hit for debugging or other purposes.
    public override RaycastHit GetRaycastHit()
    {
        return CurrentHit;
    }

    // These methods are used for initialization and enabling/disabling the input.
    private void Awake()
    {
        PlayerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        PlayerControls.PlayerActionMap.PlayerMove.Enable();
    }

    private void Update()
    {
        // Update the input position every frame.
        GetInputPosition();    
    }

    private void OnDisable()
    {
        PlayerControls.PlayerActionMap.PlayerMove.Disable();
    }
}

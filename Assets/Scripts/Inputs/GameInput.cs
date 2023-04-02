using UnityEngine;

// This is the abstract base class for all game input types.
public abstract class GameInput : MonoBehaviour
{
    // These fields are used to store common input information.
    private PlayerControls playerControls;
    private bool inputStatus;
    private Vector3 inputPosition;
    private Ray currentRay;
    private RaycastHit currentHit;


    // These properties are used to store common input information.
    protected PlayerControls PlayerControls { get { return playerControls; } set { playerControls = value; } }
    protected bool InputStatus { get { return inputStatus; } set { inputStatus = value; } }
    protected Vector3 InputPosition { get { return inputPosition; } set { inputPosition = value; } }
    protected Ray CurrentRay { get { return currentRay; } set { currentRay = value; } }
    protected RaycastHit CurrentHit { get { return currentHit; } set { currentHit = value; } }

    // These abstract methods must be implements by subclass/es.
    public abstract bool GetInputStatus();
    public abstract Vector3 GetInputPosition();
    public abstract Ray GetRay();
    public abstract RaycastHit GetRaycastHit();
}

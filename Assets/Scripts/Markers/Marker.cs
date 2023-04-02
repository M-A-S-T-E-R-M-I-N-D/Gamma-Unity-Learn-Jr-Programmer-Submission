using UnityEngine;

// Abstract class that serves as a base for other marker types.
public abstract class Marker : MonoBehaviour
{
    // These fields are used to store common marker information.
    [SerializeField] private Transform markerTransform;
    private Collider groundCollider;

    // These protected properties can be accessed by derived classes but not by other classes.
    protected Transform MarkerTransform { get { return markerTransform; } set { markerTransform = value; } }
    protected Collider GroundCollider { get { return groundCollider; } set { groundCollider = value; } }

    // Find ground collider if it's not already set.
    private void FindGround()
    {
        if (groundCollider == null)
        {
            var ground = GameObject.FindGameObjectWithTag("Ground");
            if (ground)
            {
                groundCollider = ground.GetComponent<Collider>();
            }
            else
            {
                // Error message if ground collider can't be found.
                Debug.LogError("Error: Can not find \"Ground\" tag. \n Make sure you have a Collider attached to a GameObject with \"Ground\" tag.");
            }
        }
    }

    // Virtual method that can be overriden by derived classes.
    protected virtual void Awake()
    {
        FindGround();
    }

    // Abstract method that must be implemented by derived classes.
    protected abstract void Update();
}

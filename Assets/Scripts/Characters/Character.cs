using UnityEngine;

// Abstract class that serves as a base for other characters.
public abstract class Character : MonoBehaviour
{
    // These fields are used to store common character information.
    private Vector3 currentPosition;
    private Vector3 destinationPosition;
    private Rigidbody charRigidbody;
    private Animator charAnimator;

    // These protected properties can be accessed by derived classes but not by other classes.
    protected Vector3 CurrentPosition { get { return currentPosition; } set { currentPosition = value; } }
    protected Vector3 DestinationPosition { get { return destinationPosition; } set { destinationPosition = value; } }
    protected Rigidbody Rigidbody { get { return charRigidbody; } set { charRigidbody = value; } }
    protected Animator Animator { get { return charAnimator; } set { charAnimator = value; } }

    // Abstract methods that must be implemented by derived classes.
    protected abstract void SetCharacterDestinationPosition(Vector3 destination);
    protected abstract void UpdateCharacterPosition();
    protected abstract void UpdateCharacterDirection(Vector3 direction);
}

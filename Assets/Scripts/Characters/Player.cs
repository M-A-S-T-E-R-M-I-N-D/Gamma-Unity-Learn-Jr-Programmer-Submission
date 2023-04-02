using UnityEngine;

// This is the implementation for a specific character: Player.
sealed class Player : Character
{
    // These Private serialize fields are visible through the inspector.
    [SerializeField] private float playerSpeed = 7.5f;
    [SerializeField] private float playerTurnSpeed = 12.5f;

    // These fields are used to store the player information.
    private bool atDestination;
    private Vector3 playerInputPosition;

    // This method will set the character's destination position, and remove at destination flag.
    protected override void SetCharacterDestinationPosition(Vector3 playerDestination)
    {
        DestinationPosition = new Vector3(playerDestination.x, transform.position.y, playerDestination.z);
        if (atDestination) atDestination = false;
    }

    // This method will update the character direction according to the destination position.
    protected override void UpdateCharacterDirection(Vector3 playerDirection)
    {
        if (playerDirection != Vector3.zero)
        {
            Quaternion destinationRotation = Quaternion.LookRotation(playerDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, destinationRotation, playerTurnSpeed * Time.deltaTime);
        }
    }

    // This method will update the characters movement, and handle the animator.
    protected override void UpdateCharacterPosition()
    {
        Vector3 movementDirection = DestinationPosition - CurrentPosition;
        movementDirection = new Vector3(movementDirection.x, transform.position.y, movementDirection.z);
        float distance = movementDirection.magnitude;

        // If the distance is very close to destintion position, stop moving, and play the idle animation.
        if (distance <= 0.1f)
        {
            Rigidbody.velocity = Vector3.zero; Rigidbody.angularVelocity = Vector3.zero;
            Animator.Play("PlayerIdle");
        }
        // Move the rigidbody in direction, set the current position and play the move animation.
        else
        {
            movementDirection.Normalize();
            Vector3 playerMovement = movementDirection * playerSpeed * Time.deltaTime;
            Rigidbody.MovePosition(transform.position + playerMovement);
            CurrentPosition = transform.position;
            Animator.Play("PlayerMove");
        }
    }

    // This method will handle the player's movement.
    private void DrawMovement()
    {
        playerInputPosition = ClickInput.Instance.GetInputPosition();

        if (transform.position != playerInputPosition && playerInputPosition != Vector3.zero)
        {
            SetCharacterDestinationPosition(playerInputPosition);
        }

        UpdateCharacterDirection(DestinationPosition - CurrentPosition);
        UpdateCharacterPosition();
    }

    // This method is used for initialization.
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();

        CurrentPosition = transform.position;
        DestinationPosition = CurrentPosition;
    }

    // Draw movement every frame update.
    private void Update()
    {
        DrawMovement();
    }
}

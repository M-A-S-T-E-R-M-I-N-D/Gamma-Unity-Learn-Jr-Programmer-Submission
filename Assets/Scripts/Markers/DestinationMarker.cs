using UnityEngine;

// This is the implementation for a specific marker: Destination Marker.
sealed class DestinationMarker : Marker
{
    // These fields are used to store the marker information.
    private Transform currentMarker;
    private Animation currentAnimation;

    // This method will rewind the marker animation, play, sample its position, stop at location and replay.
    private void ReplayLegacyAnimation()
    {
        currentAnimation.Rewind();
        currentAnimation.Play();
        currentAnimation.Sample();
        currentAnimation.Stop();
        currentAnimation.Play();
    }

    // This method will handle (move or instantiate) the marker instance.
    private void HandleDestinationMarker()
    {
        if (ClickInput.Instance.GetInputStatus())
        {
            Ray ray = ClickInput.Instance.GetRay();
            RaycastHit hit;

            Vector3 clickPosition = ClickInput.Instance.GetInputPosition();

            if (Physics.Raycast(ray, out hit) && hit.collider == GroundCollider)
            {
                if (currentMarker != null)
                {
                    currentMarker.transform.position = clickPosition;
                    currentMarker.transform.parent = GroundCollider.transform;

                    currentAnimation = currentMarker.GetComponentInChildren<Animation>();

                    ReplayLegacyAnimation();
                }
                else
                {
                    currentMarker = Instantiate(MarkerTransform, clickPosition, Quaternion.identity, GroundCollider.transform);
                }
            }
        }
    }

    // Common Awake method (use base.Awake(); of Marker.cs).
    protected override void Awake() => base.Awake();

    // Update method that creates or moves the marker on click.
    protected override void Update()
    {
        HandleDestinationMarker();
    }
}

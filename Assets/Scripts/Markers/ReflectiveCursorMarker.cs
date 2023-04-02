using UnityEngine;

// This is the implementation for a specific marker: Reflective Cursor Marker.
sealed class ReflectiveCursorMarker : Marker
{
    // These Private serialize fields are visible through the inspector.
    [SerializeField] private Color groundColor;
    [SerializeField] private Color voidColor;
    [SerializeField] private float colorTransitionSpeed;

    // These fields are used to store the marker information.
    private static bool cursorIsVisible;
    private Renderer markerRenderer;
    private bool isGround;
    private Color reflectiveColor;

    // This method set the visibility of the cursor.
    private void SetCursorVisibility(bool visible)
    {
        cursorIsVisible = visible;
        Cursor.visible = cursorIsVisible;
    }

    // This method keeps the marker on screen by adjusting its position.
    private void CheckMarkerOnScreen()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(MarkerTransform.position);

        if (!cursorIsVisible && viewportPos.x < 0.0f || viewportPos.x > 1.0f || viewportPos.y < 0.0f || viewportPos.y > 1.0f)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(MarkerTransform.position);
            screenPos.x = Mathf.Clamp(screenPos.x, 0.0f, Screen.width);
            screenPos.x = Mathf.Clamp(screenPos.y, 0.0f, Screen.height);
            MarkerTransform.position = Camera.main.ScreenToWorldPoint(screenPos);
        }
    }

    // This method changes the position and the color of the marker according to ground.
    private void UpdateMarker()
    {
        Ray ray = ClickInput.Instance.GetRay();
        RaycastHit hit;

        isGround = Physics.Raycast(ray, out hit) && hit.collider == GroundCollider;

        Vector3 markerPosition = isGround ? new Vector3(hit.point.x, transform.position.y, hit.point.z) : new Vector3(hit.point.x, transform.position.y, hit.point.z);

        reflectiveColor = isGround ? groundColor : voidColor;

        Color currentColor = markerRenderer.material.color;
        markerRenderer.material.color = Color.Lerp(currentColor, reflectiveColor, colorTransitionSpeed * Time.deltaTime);

        CheckMarkerOnScreen();

        MarkerTransform.position = markerPosition;
    }

    // These methods are used for initialization.
    protected override void Awake()
    {
        base.Awake();

        if (MarkerTransform == null)
        {
            MarkerTransform = GetComponent<Transform>();
        }

        markerRenderer = GetComponentInChildren<Renderer>();
    }

    // Disable the cursor on enable.
    private void OnEnable() => SetCursorVisibility(false);

    private void Start() => SetCursorVisibility(false);

    // Update method updates the marker position and color.
    protected override void Update()
    {
        UpdateMarker();
    }

    // Enable the cursor on disable.
    private void OnDisable() => SetCursorVisibility(true);
}

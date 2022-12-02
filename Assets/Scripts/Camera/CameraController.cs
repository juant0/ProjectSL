using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private PlayerInputActions.MouseActions mouseActions;

    [SerializeField] private Transform targetLookAtTransform;

    private Vector2 mouseInput;

    private void Awake()
    {
        InitilizeInputActions();
        Cursor.visible = false;
        transform.LookAt(targetLookAtTransform);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();

    }

    private void Update()
    {
        mouseInput = mouseActions.Position.ReadValue<Vector2>();
        targetLookAtTransform.rotation = Quaternion.Euler(0, mouseInput.x % 360, 0);
    }

    private void InitilizeInputActions()
    {
        inputActions = new PlayerInputActions();
        mouseActions = inputActions.Mouse;
    }
}

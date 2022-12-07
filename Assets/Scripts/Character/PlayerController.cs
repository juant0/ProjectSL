using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private PlayerInputActions.MovementActions movementActions;
    [Header("Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private float jumpHeight = 5;

    private Transform model;

    private RigidbodyHandler rigibodyHandler;
    private Vector3 movementDirection;
    private Camera mainCamera;

    private GroundHandler groundHandler;

    private JetPackHandle jeckPackHandle;
    private StickHandle stickHandle;

    private void Awake()
    {
        rigibodyHandler = GetComponent<RigidbodyHandler>();
        mainCamera = Camera.main;
        model = transform.GetChild(0);
        groundHandler = GetComponent<GroundHandler>();
        jeckPackHandle = GetComponent<JetPackHandle>();
        stickHandle = GetComponent<StickHandle>();
        InitilizeInputActions();

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
        movementDirection = GetMovementDirection();
        Rotate();
        if (stickHandle.IsStick && !groundHandler.OnGround)
            return;
        rigibodyHandler.ForceDirection += movementDirection * speed ;
    }

    private void Rotate()
    {
        Vector3 rotateDirection = GetRotationDirection();
        Vector3 rbVelocity = rigibodyHandler.Rb.velocity;
        rbVelocity.y = 0;
        model.transform.Rotate(rotateDirection, rbVelocity.magnitude * speed * 10 * Time.deltaTime, Space.World);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 movementDirection = mainCamera.transform.forward * movementActions.Move.ReadValue<Vector2>().y + mainCamera.transform.right * movementActions.Move.ReadValue<Vector2>().x;
        movementDirection.y = 0;
        return movementDirection;
    }
    private Vector3 GetRotationDirection() => Vector3.right * rigibodyHandler.Rb.velocity.z - Vector3.forward * rigibodyHandler.Rb.velocity.x;


    /// <summary>
    ///  Initialize the InputSystem and assigns the respective functions to each input
    ///  To know what are the keys or change the keys you should go => Assets/InputAction/PlayerInputActions
    /// </summary>
    private void InitilizeInputActions()
    {
        inputActions = new PlayerInputActions();
        movementActions = inputActions.Movement;
        movementActions.Jump.performed += context => Jump();
        movementActions.Jump.performed += context => stickHandle.JumpStick();
        movementActions.JeckPack.started += context => jeckPackHandle.StartBoost();
        movementActions.JeckPack.canceled += context => jeckPackHandle.StopBoost();
    }

    private void Jump()
    {
        if (!groundHandler.OnGround )
            return;
        StartCoroutine(Jumping());
    }

    private IEnumerator Jumping() {
        float startHeight = transform.position.y;
        float lerpTime = 0;
        while ( transform.position.y < startHeight + jumpHeight && !stickHandle.IsStick) {
            lerpTime = transform.position.y - startHeight / jumpHeight;
            rigibodyHandler.ForceDirection.y += Mathf.Lerp(jumpSpeed, jumpSpeed * 0.5f, lerpTime) * 2;         
            yield return null;
        }
    }
}

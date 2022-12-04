using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private PlayerInputActions.MovementActions movementActions;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private float jumpHeight = 5;

    private Transform model;

    private RigibodyHandler rigibodyHandler;
    private Vector3 movementDirection;
    private Camera mainCamera;

    private GroundHandler groundHandler;
    private bool canJumpn = true;

    private JeckPackHandle jeckPackHandle;
    private StickHandle stickHandle;

    private void Awake()
    {
        rigibodyHandler = GetComponent<RigibodyHandler>();
        mainCamera = Camera.main;
        model = transform.GetChild(0);
        groundHandler = GetComponent<GroundHandler>();
        jeckPackHandle = GetComponent<JeckPackHandle>();
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
        rigibodyHandler.ForceDirection += movementDirection * speed * Time.deltaTime;
    }

    private void Rotate()
    {
        Vector3 rotateDirection = GetRotationDirection();
        model.transform.Rotate(rotateDirection, speed, Space.World);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 movementDirection = mainCamera.transform.forward * movementActions.Move.ReadValue<Vector2>().y + mainCamera.transform.right * movementActions.Move.ReadValue<Vector2>().x;
        movementDirection.y = 0;
        return movementDirection;
    }
    private Vector3 GetRotationDirection() => Vector3.right * rigibodyHandler.Rb.velocity.z - Vector3.forward * rigibodyHandler.Rb.velocity.x;

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
        Debug.Log("jujmps");

        if (!groundHandler.OnGround  || !canJumpn)
            return;
        canJumpn = false;
        StartCoroutine(Jumping());
        StartCoroutine(ActiveAfterFrame());
    }

    private IEnumerator Jumping() {
        float startHeight = transform.position.y;
        while ( transform.position.y <= startHeight + jumpHeight && !stickHandle.IsStick) {
            rigibodyHandler.ForceDirection.y += jumpSpeed * 2* Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ActiveAfterFrame()
    { 
        yield return new WaitForSeconds(0.5f);
        canJumpn = true;
    }
}

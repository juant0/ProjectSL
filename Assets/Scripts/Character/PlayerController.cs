using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private PlayerInputActions.MovementActions movementActions;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private float jumpHeight = 5;

    private Transform model;

    private Rigidbody rb;
    private Vector3 movementDirection;
    private Camera mainCamera;

    private GroundHandler groundHandler;

    private void Awake()
    {
        InitilizeInputActions();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        model = transform.GetChild(0);
        groundHandler = GetComponent<GroundHandler>();
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
        Vector3 rotateDirection = GetRotationDirection();
        model.transform.Rotate(rotateDirection, speed, Space.World);
    }
    private void FixedUpdate()
    {
        Move();
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 movementDirection = mainCamera.transform.forward * movementActions.Move.ReadValue<Vector2>().y + mainCamera.transform.right * movementActions.Move.ReadValue<Vector2>().x;
        movementDirection.y = 0;
        return movementDirection;
    }
    private Vector3 GetRotationDirection() => Vector3.right * movementDirection.z - Vector3.forward * movementDirection.x;

    private void Move()
    {
        Vector3 rigibodyVelocity = rb.velocity;
        rigibodyVelocity.y = 0;
        rb.AddForce(movementDirection * speed - rigibodyVelocity, ForceMode.VelocityChange);
    }

    private void InitilizeInputActions()
    {
        inputActions = new PlayerInputActions();
        movementActions = inputActions.Movement;
        movementActions.Jump.performed += context => Jump();
    }

    private void Jump()
    {
        if (!groundHandler.OnGround)
            return;
        StartCoroutine(Jumping());
        Debug.Log("jujmps");
    }

    private IEnumerator Jumping() {
        float startHeight = transform.position.y;
        movementDirection.y += jumpSpeed;
        while ( transform.position.y <= startHeight + jumpHeight) {
            movementDirection.y += jumpSpeed * Time.deltaTime;
            yield return null;
        }
    }
}

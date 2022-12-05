using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickHandle : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float stickStrength = 1;
    [SerializeField] private float reduceStrenghtRate;
    [SerializeField] private LayerMask stickLayer;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpDistance;

     public bool IsStick = false;
    private bool isJumpingStick;
    [SerializeField]private float currentStickStrength;
    private RigibodyHandler rigibodyHandler;
    private GroundHandler groundHandler;
    private float timer;

    private Vector3 contactlDirection;

    private Coroutine jumpingCoroutine;
    private void Awake()
    {
        rigibodyHandler = GetComponent<RigibodyHandler>();
        groundHandler = GetComponent<GroundHandler>();
    }
    private void OnCollisionEnter(Collision collision)
    {
       
       if(!LayerUtilities.IsSameLayer(stickLayer,collision.gameObject.layer))
            return;
        currentStickStrength = Physics.gravity.y - Physics.gravity.y * stickStrength;
        IsStick = true;
        contactlDirection = collision.contacts[0].normal;
        contactlDirection.y = 0;
        contactlDirection.Normalize();
        rigibodyHandler.SetRigiBodyVelocity(Vector3.zero);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!LayerUtilities.IsSameLayer(stickLayer, collision.gameObject.layer))
            return;
        IsStick = false;
    }
    private void Update()
    {
        if (!IsStick || groundHandler.OnGround)
            return;
        if (isJumpingStick)
            return;
        rigibodyHandler.SetRigiBodyVelocity(Vector3.up * currentStickStrength);
        timer += Time.deltaTime;
        if (timer > reduceStrenghtRate)
        {
            timer = 0;
            currentStickStrength = Mathf.Clamp(currentStickStrength - Time.deltaTime ,Physics.gravity.y,0) ;
        }
    }
    public void JumpStick() 
    {
        if (isJumpingStick)
            return;
        if (!IsStick)
            return;
        IsStick = false;
        if (jumpingCoroutine != null)
            StopCoroutine(jumpingCoroutine);
        jumpingCoroutine = StartCoroutine(Jumping((Vector3.up + contactlDirection).normalized));
    }
    private IEnumerator Jumping(Vector3 direction)
    {
        Vector3 startPosition = transform.position;
        rigibodyHandler.SetRigiBodyVelocity(Vector3.zero);
        float lerpTime = 0;
        float distance = Vector3.Distance(transform.position, startPosition);
        direction *= jumpSpeed;
        while (distance < jumpDistance && !IsStick && !groundHandler.OnGround) 
        {
            distance = Vector3.Distance(transform.position, startPosition);
            lerpTime = distance / jumpDistance;
            Debug.Log(lerpTime);
            rigibodyHandler.ForceDirection += Vector3.Lerp(direction, direction * 0.5f, lerpTime) * 2;
            yield return null;
        }
        isJumpingStick = false;
    }

}

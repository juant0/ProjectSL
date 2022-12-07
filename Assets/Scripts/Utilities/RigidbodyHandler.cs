using UnityEngine;

public class RigidbodyHandler : MonoBehaviour
{
    public Rigidbody Rb { get; private set; }
    [HideInInspector] public Vector3 ForceDirection = Vector3.zero;
    private GroundHandler GroundHandler;
    private float fallingTimer = 0;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        GroundHandler = GetComponent<GroundHandler>();
    }
    public void Update()
    {
        AddForce();
        ForceDirection = Vector3.zero;
    }

    private void AddForce()
    {
        AddGravityForce();
        Rb.AddForce((ForceDirection - Rb.velocity ) * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void AddGravityForce()
    {
        if (ForceDirection.y == 0 && !GroundHandler.OnGround)
        {
            ForceDirection += Physics.gravity * fallingTimer;
            fallingTimer += Time.deltaTime;
        }
        else
            fallingTimer = 0;
    }

    public void SetRigidBodyVelocity(Vector3 newRbVelocity) => Rb.velocity = newRbVelocity;
}

using UnityEngine;

public class RigidbodyHandler : MonoBehaviour
{
    public Rigidbody Rb { get; private set; }
    [HideInInspector] public Vector3 ForceDirection = Vector3.zero;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        AddForce();
        ForceDirection = Vector3.zero;
    }

    private void AddForce()
    {
        Vector3 rbVelocity = Rb.velocity;
        if (ForceDirection.y == 0)
            ForceDirection += Physics.gravity;
        Rb.AddForce((ForceDirection - rbVelocity * 0.9f) * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    public void SetRigidBodyVelocity(Vector3 newRbVelocity) => Rb.velocity = newRbVelocity;
}

using UnityEngine;

public class RigibodyHandler : MonoBehaviour
{
    public Rigidbody Rb { get; private set; }
    [HideInInspector] public Vector3 ForceDirection = Vector3.zero;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        Vector3 rbVelocity = Rb.velocity;
        if (ForceDirection.y == 0)
            ForceDirection += Physics.gravity;
        Rb.AddForce((ForceDirection - rbVelocity * 0.9f) * Time.deltaTime, ForceMode.VelocityChange);
        ForceDirection = Vector3.zero;
    }

    public void SetRigiBodyVelocity(Vector3 newRbVelocity) => Rb.velocity = newRbVelocity;
}

using UnityEngine;

public class RigibodyHandler : MonoBehaviour
{
    public Rigidbody Rb {get; private set;}
    [HideInInspector]public Vector3 ForceDirection = Vector3.zero;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        if (ForceDirection == Vector3.zero)
            return;
        Vector3 rbVelocity = Rb.velocity;
        if(ForceDirection.y == 0)
            rbVelocity.y = 0;
        Rb.AddForce(10 * ForceDirection - rbVelocity, ForceMode.VelocityChange);
        ForceDirection = Vector3.zero;
    }

    public void SetRigiBodyVelocity(Vector3 newRbVelocity) => Rb.velocity = newRbVelocity;
}

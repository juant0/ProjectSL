using UnityEngine;

public class RigibodyHandler : MonoBehaviour
{
    private Rigidbody rb;
    [HideInInspector]public Vector3 ForceDirection = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        Vector3 rbVelocity = rb.velocity;
        rbVelocity.y = 0;
        rb.AddForce(10 * ForceDirection - rbVelocity, ForceMode.VelocityChange);
        ForceDirection = Vector3.zero;
    }
}

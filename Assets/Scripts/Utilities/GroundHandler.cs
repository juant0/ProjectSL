using UnityEngine;

public class GroundHandler : MonoBehaviour
{
    [Header("Detect Ground Settings")]
    [Tooltip("Origin of the check")]
    [SerializeField] private Transform transformOrigin;
    [Tooltip("Defines what layer the object must have to be detected")]
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("Defines the radius of the ground check, It's should be a little less than player radius Collider")]
    [SerializeField] private float collideRadius = 0.5f;
    [Tooltip("Defines the max distance to detech the floor, It's shoul be a small value")]
    [SerializeField] private float rayCastDistance = 0.5f;
    [field:SerializeField]public  bool OnGround { get; private set; }
    private void FixedUpdate()
    {
        OnGround = Physics.SphereCast(transformOrigin.position, collideRadius, Vector3.down, out RaycastHit hit, rayCastDistance);
    }
}

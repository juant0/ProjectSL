using UnityEngine;

public class GroundHandler : MonoBehaviour
{
    [Header("Detect Ground Settings")]
    [Tooltip("Origin of the check")]
    [SerializeField] private Transform transformOrigin;
    [Tooltip("Defines what layer the object must have to be detected")]
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("Defines the radiues of the ground check, It's should be a little less than player radiues Collider")]
    [SerializeField] private float collideRadius = 0.5f;
    [Tooltip("Defines the max distance to detech the floor")]
    [SerializeField] private float rayCastDistance = 0.5f;
    public  bool OnGround { get; private set; }
    private void FixedUpdate()
    {
        OnGround = Physics.SphereCast(transformOrigin.position, collideRadius, Vector3.down, out RaycastHit hit, rayCastDistance);
    }
}

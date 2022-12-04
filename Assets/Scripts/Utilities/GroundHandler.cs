using UnityEngine;

public class GroundHandler : MonoBehaviour
{
    [SerializeField] private Transform transformOrigin;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float collideRadius = 0.5f;
    [SerializeField] private float rayCastDistance = 0.5f;
    [field:SerializeField] public  bool OnGround { get; private set; }
    private void FixedUpdate()
    {
        SetIsOnGround(); 
    }

    private void SetIsOnGround()
    {
        OnGround = Physics.SphereCast(transformOrigin.position , collideRadius, Vector3.down, out RaycastHit hit, rayCastDistance );
    }
}

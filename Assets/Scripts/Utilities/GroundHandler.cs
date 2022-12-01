using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHandler : MonoBehaviour
{
    [SerializeField] private Transform transformOrigin;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float collideRadius = 0.5f;
    [SerializeField] private float rayCastDistance = 0.5f;
    public  bool OnGround { get; private set; }
    RaycastHit hit;
    private void Awake()
    {
        
    }
    private void FixedUpdate()
    {
        SetIsOnGround(); 
    }

    private void SetIsOnGround()
    {
        OnGround = Physics.SphereCast(transformOrigin.position , collideRadius, Vector3.down, out hit, rayCastDistance );
    }
}

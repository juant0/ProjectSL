using System;
using UnityEngine;

public class JetPackHandle : MonoBehaviour
{
    [Header("Jeckpack Settings")]
    [Tooltip("Every x time the boost recharge when the player is on the ground")]
    [SerializeField] private float rechargeRate;
    [Tooltip("Jetpack works with amount a fuel that it's reducing every frame when you are using and it's recharge when the player is on the ground ")]
    [field: SerializeField] public int FuelAmount { get; private set; }
    [Tooltip("Upware force aplied on the player  ")]
    [SerializeField] private float boostStrength;

    [Header("Effects")]
    [Tooltip("Particle system effect that it's played when it is ussing the jeckpack")]
    [SerializeField] private ParticleSystem effectTrailRender;


    private float currentFuel = 0;

    private GroundHandler groundHandler;

    private RigidbodyHandler rigibodyHandler;

    public delegate void OnPlayerFloatChanged(float floatToChange);
    public OnPlayerFloatChanged OnPlayerFuelChange;

    private bool isBoosting = false;
    private float timer = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        groundHandler = GetComponent<GroundHandler>();
        rigibodyHandler = GetComponent<RigidbodyHandler>();
        SetFuel(FuelAmount);
        effectTrailRender.Stop();
    }

    void Update()
    {
        FuelHandle();
        if (isBoosting)
            Boost();
    }
    private void FuelHandle()
    {
        if (!groundHandler.OnGround || isBoosting)
            return;
        timer += Time.deltaTime;
        if (timer > rechargeRate)
        {
            timer = 0;
            SetFuel(1);
        }
    }
    private void Boost()
    {
        if (!isBoosting || currentFuel <= 0)
            return;
        rigibodyHandler.ForceDirection += Vector3.up * boostStrength;
        SetFuel(-Time.deltaTime);
    }
    public void StartBoost()
    {
        if (currentFuel == 0)
            return;
        isBoosting = true;
        effectTrailRender.Play();
    }

    public void StopBoost()
    {
        isBoosting = false;
        effectTrailRender.Stop();
    }
    private void SetFuel(float boostAmount) 
    {
        currentFuel = Math.Clamp(currentFuel + boostAmount, 0, FuelAmount);
        if(currentFuel == 0)
            Debug.Log(transform.position.y);
        OnPlayerFuelChange?.Invoke(currentFuel);
    }
}

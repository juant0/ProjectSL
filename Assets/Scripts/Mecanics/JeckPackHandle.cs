using System;
using UnityEngine;

public class JeckPackHandle : MonoBehaviour
{
    [SerializeField] private float rechargeRate;
    [field: SerializeField] public int FuelAmount { get; private set; }
    [SerializeField] private float boostStrength;

    private float currentFuel = 0;

    private GroundHandler groundHandler;

    private RigibodyHandler rigibodyHandler;

    public delegate void OnPlayerFloatChanged(float floatToChange);
    public OnPlayerFloatChanged OnPlayerFuelChange;

    private bool isBoosting = false;
    private float timer = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        groundHandler = GetComponent<GroundHandler>();
        rigibodyHandler = GetComponent<RigibodyHandler>();
        SetBoost(FuelAmount);
    }

    void Update()
    {
        BoostHandle();
        if (isBoosting)
            Boost();
    }

    private void BoostHandle()
    {
        if (!groundHandler.OnGround || isBoosting)
            return;
        timer += Time.deltaTime;
        if (timer > rechargeRate)
        {
            timer = 0;
            SetBoost(1);
        }
    }

    private void Boost()
    {
        if (!isBoosting || currentFuel <= 0)
            return;
        rigibodyHandler.ForceDirection += Vector3.up * boostStrength * Time.deltaTime;
        SetBoost(-Time.deltaTime);
    }
    public void StartBoost()
    {
        if (/*groundHandler.OnGround ||*/ currentFuel == 0)
            return;
        isBoosting = true;
    }

    public void StopBoost() => isBoosting = false;
    private void SetBoost(float boostAmount) 
    {
        currentFuel = Math.Clamp(currentFuel + boostAmount, 0, FuelAmount);
        if(currentFuel == 0)
            Debug.Log(transform.position.y);
        OnPlayerFuelChange?.Invoke(currentFuel);
    }
}

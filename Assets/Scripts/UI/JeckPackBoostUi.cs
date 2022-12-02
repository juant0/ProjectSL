using UnityEngine;
using UnityEngine.UI;

public class JeckPackBoostUi : MonoBehaviour
{
    [SerializeField] private Image fillImageBoost;
    [SerializeField] private Text TextBoost;
    [SerializeField] private JeckPackHandle jeckPackHandle;
    private void Awake()
    {
        jeckPackHandle.OnPlayerFuelChange += UpdateBoostUI;
    }

    private void UpdateBoostUI(float boostValue)
    {
        fillImageBoost.fillAmount = boostValue / jeckPackHandle.FuelAmount;
        TextBoost.text = $"{boostValue.ToString("0.00")}/{jeckPackHandle.FuelAmount}";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeterScript : MonoBehaviour
{
    public Slider energyMeter;
    public Image fillAreaImage;

    Color validPowerAmount;
    Color invalidPowerAmount;

    private float maxPower = 100;
    private float currentPower;
    public bool enoughPower;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static EnergyMeterScript instance;

    // Prepares an instance in 'PlayerController' to subtract power from the slider
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentPower = maxPower;
        energyMeter.maxValue = maxPower;
        energyMeter.value = maxPower;

        validPowerAmount = new Color(0, 1, 255);
        invalidPowerAmount = new Color(255, 0, 0);

    }

    public void DrainPower(float amount)
    {
        if (currentPower - amount >= PlayerController.instance.addedDirectionalForce)
        {
            currentPower -= amount;
            energyMeter.value = currentPower;

            if (regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(RegenPower());
        }
        else
        {
            PlayerController.instance.requiredPower = false;
            fillAreaImage.color = invalidPowerAmount;
            Debug.Log("Insufficient Power");

        }
    }

    private void Update()
    {
        if (currentPower >= 30)
        {
            PlayerController.instance.requiredPower = true;
            fillAreaImage.color = validPowerAmount;
        }
    }


    private IEnumerator RegenPower()
    {
        yield return new WaitForSeconds(0.1f);

        while (currentPower < maxPower)
        {
            currentPower += maxPower / 80;
            energyMeter.value = currentPower;
            yield return regenTick;
        }
        regen = null;
    }
}

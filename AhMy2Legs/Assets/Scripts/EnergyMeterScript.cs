using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeterScript : MonoBehaviour
{
    public Slider energyMeter; // Calls slider
    public Image fillAreaImage; // Calls fill area
    
    Color validPowerAmount; // Sets fill area to blue
    Color invalidPowerAmount; // Sets fill area to red

    private float maxPower = 100; // Initiates max slider value
    private float currentPower; // tracks current power value

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f); // custom timer set to 0.1 seconds
    private Coroutine regen; // references a coroutine for regenning power

    public static EnergyMeterScript instance; // instances the script for 'PlayerController' to use

    // Prepares an instance in 'PlayerControllerScript' to subtract power from the slider
    private void Awake()
    {
        instance = this;
    }

    // Sets current values that were ambiguous earlier.
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
        // Function that takes 'addedDirectionalForce' from 'playerController' and uses it to remove a amount
        // from the meter. Also has a bool which can prevent firing of the slingshot and colour changes
        if (currentPower - amount >= PlayerControllerScript.instance.addedDirectionalForce)
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
            PlayerControllerScript.instance.requiredPower = false;
            fillAreaImage.color = invalidPowerAmount;
        }
    }

    // Resets 'requiredPower' to avoid a soft-lock, also resets fillArea colour
    private void Update()
    {
        if (currentPower >= 30)
        {
            PlayerControllerScript.instance.requiredPower = true;
            fillAreaImage.color = validPowerAmount;
        }
    }

    // Initates a regeneration of the meter shortly after it has been consumed.
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

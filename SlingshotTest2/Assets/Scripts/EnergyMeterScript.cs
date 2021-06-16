using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeterScript : MonoBehaviour
{
    public Slider energyMeter;

    public void SetEnergy(int energy)
    {
        energyMeter.value = energy;
    }

}

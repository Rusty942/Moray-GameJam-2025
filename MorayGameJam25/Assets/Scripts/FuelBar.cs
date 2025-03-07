using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public Slider fuelSlider;

    public void setMaxFuel(int fuel)
    {
        fuelSlider.maxValue = fuel;
        fuelSlider.value = fuel;
    }

    public void setFuel(int fuel)
    {
        fuelSlider.value = fuel;
    }
}

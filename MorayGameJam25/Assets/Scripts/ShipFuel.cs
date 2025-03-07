using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFuel : MonoBehaviour
{
    public int maxFuel = 500;
    public int currentFuel;
    public FuelBar fuelBar;

    private void Start()
    {
        currentFuel = maxFuel;
        fuelBar.setMaxFuel(maxFuel);
    }
}

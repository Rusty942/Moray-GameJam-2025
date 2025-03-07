using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFuel : MonoBehaviour
{
    public int maxFuel = 500;
    public int currentFuel;
    public FuelBar fuelBar;
    
    [SerializeField] private int idleFuelConsumption = 1;
    [SerializeField] private int movingFuelConsumption = 2;
    
    private Rigidbody2D rb;

    private void Start()
    {
        currentFuel = maxFuel;
        fuelBar.setMaxFuel(maxFuel);
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ConsumeFuel());
    }
    
    private IEnumerator ConsumeFuel()
    {
        while (currentFuel > 0)
        {
            int fuelRate = rb.velocity.magnitude > 0 ? movingFuelConsumption : idleFuelConsumption;
            currentFuel = Mathf.Max(currentFuel - fuelRate, 0);
            fuelBar.setFuel(currentFuel);
            yield return new WaitForSeconds(1f);
        }
    }
}

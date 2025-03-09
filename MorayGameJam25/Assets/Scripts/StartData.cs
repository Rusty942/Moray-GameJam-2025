using System.Collections.Generic;
using UnityEngine;

public static class StartData
{
    public static Vector3 playerSpawnLocation;
    public static int currencyAmm;
    public static int fuelAmm;
    public static List<string> ingredients; 

    static StartData()
    {
        playerSpawnLocation = new Vector3(0f, 0f, 0f);
        currencyAmm = 100;
        fuelAmm = 100;
        ingredients = new List<string>();
    }
}

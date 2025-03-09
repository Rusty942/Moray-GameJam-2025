using UnityEngine;

public static class StartData
{
    public static Vector3 playerSpawnLocation;
    public static int currencyAmm;
    public static int fuelAmm;

    static StartData()
    {
        playerSpawnLocation = new Vector3(0f, 0f, 0f);
        currencyAmm = 50;
        fuelAmm = 100;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "PointManager", menuName = "Scriptable Objects/PointManager")]
public class PointManager : ScriptableObject
{
    public float cash = 1000;
    public int reputation = 0;
    public int resolved = 0;
    public int unresolve = 0;
    public int day = 0;
    private float multiplier = 0;
    
    public int PlusPoint()
    {
        reputation++;
        resolved++;

        multiplier = (reputation / 20) * 0.2f;

        float reward = 100 + 100 * multiplier;

        cash += reward;

        return Mathf.RoundToInt(reward);
    }
    public int MinusPoint()
    {
        reputation--;
        unresolve++;

        multiplier = (reputation / 20) * 0.2f;

        float punish = 100 + 100 * multiplier;

        cash -= punish;

        return Mathf.RoundToInt(punish);
    }
    public void UsePoint(VehicleType vehicleType)
    {
        cash -= Status.GetVehicleTypeCost(vehicleType);
    }
}

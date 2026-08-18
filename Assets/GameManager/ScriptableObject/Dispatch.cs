using UnityEngine;

[CreateAssetMenu(fileName = "Dispatch", menuName = "Scriptable Objects/Dispatch")]
public class Dispatch : ScriptableObject
{   
    public UnitType unitType;
    public VehicleType vehicleType;
}

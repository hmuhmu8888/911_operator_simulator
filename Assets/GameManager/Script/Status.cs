using UnityEngine;
using System.Collections.Generic;

public enum VehicleStatus
{
	OnPatrol, WaitOrder, OnRoute, OnScene, BackToPatrol, FindNewPatrol
}
public enum UnitType
{
    Police, Medical, Fire
}
public enum VehicleType
{
    PoliceBike, PoliceCar, PoliceHelicopter,   MedicalCar, Ambulance, MedicalHeicopter,   FirePickupTruck, FireTruck, FiremanHelicopter
}

public class Status : MonoBehaviour
{

    public static Sprite GetVehicleStatusSprite(VehicleStatus vehicleStatus)
    {
        switch (vehicleStatus)
        {
            default:
            case VehicleStatus.OnPatrol:        return GameAssets.i.OnPatrol;
            case VehicleStatus.WaitOrder:       return GameAssets.i.WaitOrder;
            case VehicleStatus.OnRoute:         return GameAssets.i.OnRoute;
            case VehicleStatus.OnScene:         return GameAssets.i.OnScene;
            case VehicleStatus.BackToPatrol:    return GameAssets.i.BackToPatrol;
            case VehicleStatus.FindNewPatrol:   return GameAssets.i.FindNewPatrol;
        }
    }

    public static Sprite GetVehicleTypeSprite(VehicleType vehicleType)
    {
        switch (vehicleType)
        {
            default:
            case VehicleType.PoliceBike:            return GameAssets.i.PoliceBike;
            case VehicleType.PoliceCar:             return GameAssets.i.PoliceCar;
            case VehicleType.PoliceHelicopter:      return GameAssets.i.PoliceHelicopter;
            case VehicleType.MedicalCar:            return GameAssets.i.MedicalCar;
            case VehicleType.Ambulance:             return GameAssets.i.Ambulance;
            case VehicleType.MedicalHeicopter:      return GameAssets.i.MedicalHeicopter;
            case VehicleType.FirePickupTruck:       return GameAssets.i.FirePickupTruck;
            case VehicleType.FireTruck:             return GameAssets.i.FireTruck;
            case VehicleType.FiremanHelicopter:     return GameAssets.i.FiremanHelicopter;
        }
    }
    public static int GetVehicleTypeCost(VehicleType vehicleType)
    {
        switch (vehicleType)
        {
            default:
            case VehicleType.PoliceBike:            return 25000;
            case VehicleType.PoliceCar:             return 30000;
            case VehicleType.PoliceHelicopter:      return 50000;
            case VehicleType.MedicalCar:            return 30000;
            case VehicleType.Ambulance:             return 40000;
            case VehicleType.MedicalHeicopter:      return 50000;
            case VehicleType.FirePickupTruck:       return 35000;
            case VehicleType.FireTruck:             return 45000;
            case VehicleType.FiremanHelicopter:     return 60000;
        }
    }

    // Heli nhanh nhất, Truck PickTruck Car Bike 6-7-8-9-10
    public static int GetVehicleSpeed(VehicleType vehicleType)
    {
        switch (vehicleType)
        {
            default:
            case VehicleType.PoliceBike:            return 6;
            case VehicleType.PoliceCar:             return 7;
            case VehicleType.PoliceHelicopter:      return 10;
            case VehicleType.MedicalCar:            return 7;
            case VehicleType.Ambulance:             return 9;
            case VehicleType.MedicalHeicopter:      return 10;
            case VehicleType.FirePickupTruck:       return 8;
            case VehicleType.FireTruck:             return 9;
            case VehicleType.FiremanHelicopter:     return 10;
        }
    }

    // Truck và Car xử lý nhanh nhất, PickTruck Heli Bike 6-7-8-9-10
    public static int GetTimeResolve(VehicleType vehicleType)
    {
        switch (vehicleType)
        {
            default:
            case VehicleType.PoliceBike:            return 9;
            case VehicleType.PoliceCar:             return 4;
            case VehicleType.PoliceHelicopter:      return 7;
            case VehicleType.MedicalCar:            return 6;
            case VehicleType.Ambulance:             return 4;
            case VehicleType.MedicalHeicopter:      return 9;
            case VehicleType.FirePickupTruck:       return 6;
            case VehicleType.FireTruck:             return 4;
            case VehicleType.FiremanHelicopter:     return 10;
        }
    }

    public static Sprite GetAccidentIconSprite(UnitType unitType)
    {
        switch (unitType)
        {
            default:
            case UnitType.Police:       return GameAssets.i.Police;
            case UnitType.Medical:      return GameAssets.i.Medical;
            case UnitType.Fire:         return GameAssets.i.Fire;
        }
    }
}

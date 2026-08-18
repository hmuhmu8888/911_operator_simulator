using UnityEngine;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    public GameObject shop;
    public PointManager point;
    //-------------------------------------
    public GameObject[] units;
    GridPath grid;
    private List<Node> walkableNodes = new List<Node>();
    int randomPosition;
    //-------------------------------------
    public Camera cam;

    void Awake()
    {
        grid = GetComponent<GridPath>();
    }
    void Start()
    {
        FindWalkableNode();
        shop.SetActive(false);
    }
    void FindWalkableNode()
    {
        foreach (Node node in grid.grid)
        {
            if (node.walkable)
            {
                walkableNodes.Add(node);
            }
        }
        Debug.Log($"số node đi đc: {walkableNodes.Count}");
    }

    public void OpenShop()
    {
        shop.SetActive(true);
    }
    public void CloseShop()
    {
        shop.SetActive(false);
    }
    public void BuyPoliceBike()
    {
        if(point.cash >= Status.GetVehicleTypeCost(VehicleType.PoliceBike))
        {
            randomPosition = Random.Range(0, walkableNodes.Count);
            Instantiate(units [0], walkableNodes [randomPosition].worldPosition, Quaternion.identity);
            cam.gameObject.transform.Translate(walkableNodes [randomPosition].worldPosition);
            point.UsePoint(VehicleType.PoliceBike);
        }
    }
    public void BuyPoliceCar()
    {
        if(point.cash >= Status.GetVehicleTypeCost(VehicleType.PoliceCar))
        {
            randomPosition = Random.Range(0, walkableNodes.Count);
            Instantiate(units [1], walkableNodes [randomPosition].worldPosition, Quaternion.identity);
            cam.gameObject.transform.Translate(walkableNodes [randomPosition].worldPosition);
            point.UsePoint(VehicleType.PoliceCar);
        }
    }
    public void BuyPoliceHeli()
    {
        if(point.cash >= Status.GetVehicleTypeCost(VehicleType.PoliceHelicopter))
        {
            randomPosition = Random.Range(0, walkableNodes.Count);
            Instantiate(units [2], walkableNodes [randomPosition].worldPosition, Quaternion.identity);
            cam.gameObject.transform.Translate(walkableNodes [randomPosition].worldPosition);
            point.UsePoint(VehicleType.PoliceHelicopter);
        }
    }
    public void BuyMedicCar()
    {
        if(point.cash >= Status.GetVehicleTypeCost(VehicleType.MedicalCar))
        {
            randomPosition = Random.Range(0, walkableNodes.Count);
            Instantiate(units [3], walkableNodes [randomPosition].worldPosition, Quaternion.identity);
            cam.gameObject.transform.Translate(walkableNodes [randomPosition].worldPosition);
            point.UsePoint(VehicleType.MedicalCar);
        }
    }
    public void BuyMedicAmbu()
    {
        if(point.cash >= Status.GetVehicleTypeCost(VehicleType.Ambulance))
        {
            randomPosition = Random.Range(0, walkableNodes.Count);
            Instantiate(units [4], walkableNodes [randomPosition].worldPosition, Quaternion.identity);
            cam.gameObject.transform.Translate(walkableNodes [randomPosition].worldPosition);
            point.UsePoint(VehicleType.Ambulance);
        }
    }
    public void BuyMedicHeli()
    {
        if(point.cash >= Status.GetVehicleTypeCost(VehicleType.MedicalHeicopter))
        {
            randomPosition = Random.Range(0, walkableNodes.Count);
            Instantiate(units [5], walkableNodes [randomPosition].worldPosition, Quaternion.identity);
            cam.gameObject.transform.Translate(walkableNodes [randomPosition].worldPosition);
            point.UsePoint(VehicleType.MedicalHeicopter);
        }
    }
    public void BuyFirePick()
    {
        if(point.cash >= Status.GetVehicleTypeCost(VehicleType.FirePickupTruck))
        {
            randomPosition = Random.Range(0, walkableNodes.Count);
            Instantiate(units [6], walkableNodes [randomPosition].worldPosition, Quaternion.identity);
            cam.gameObject.transform.Translate(walkableNodes [randomPosition].worldPosition);
            point.UsePoint(VehicleType.FirePickupTruck);
        }
    }
    public void BuyFireTruck()
    {
        if(point.cash >= Status.GetVehicleTypeCost(VehicleType.FireTruck))
        {
            randomPosition = Random.Range(0, walkableNodes.Count);
            Instantiate(units [7], walkableNodes [randomPosition].worldPosition, Quaternion.identity);
            cam.gameObject.transform.Translate(walkableNodes [randomPosition].worldPosition);
            point.UsePoint(VehicleType.FireTruck);
        }
    }
    public void BuyFireHeli()
    {
        if(point.cash >= Status.GetVehicleTypeCost(VehicleType.FiremanHelicopter))
        {
            randomPosition = Random.Range(0, walkableNodes.Count);
            Instantiate(units [8], walkableNodes [randomPosition].worldPosition, Quaternion.identity);
            cam.gameObject.transform.Translate(walkableNodes [randomPosition].worldPosition);
            point.UsePoint(VehicleType.FiremanHelicopter);
        }
    }
}

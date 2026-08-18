using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i == null)
                _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();

            return _i;
        }
    }

    [Header ("Vehicle Status")]
    public Sprite OnPatrol;
    public Sprite WaitOrder;
    public Sprite OnRoute;
    public Sprite OnScene;
    public Sprite BackToPatrol;
    public Sprite FindNewPatrol;

    [Header ("Vehicle Type")]
    public Sprite PoliceBike;
    public Sprite PoliceCar;
    public Sprite PoliceHelicopter;
    public Sprite MedicalCar;
    public Sprite Ambulance;
    public Sprite MedicalHeicopter;
    public Sprite FirePickupTruck;
    public Sprite FireTruck;
    public Sprite FiremanHelicopter;

    [Header ("Unit Type")]
    public Sprite Police;
    public Sprite Medical;
    public Sprite Fire;
}

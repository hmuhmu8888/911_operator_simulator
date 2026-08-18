using UnityEngine;
using System.Collections;

public class Accident : MonoBehaviour
{
    // public AccidentManager accidentManager;
    public PointManager point;
    private int timeResolve;
    Coroutine accidentCoroutine;
    public Incident incident;
    UnitType unitType;
    public GameObject icon;

    void Start()
    {
        Debug.Log("đã xuất hiện tai nạn!");
        accidentCoroutine = StartCoroutine(MissionFail());
        unitType = incident.unitType;
        icon.GetComponent<SpriteRenderer>().sprite = Status.GetAccidentIconSprite(unitType);
        Debug.Log($"tai nạn này cần unit {unitType}");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Click chuột phải
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    Debug.Log("A is target");
                    GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
                    Debug.Log($"có {units.Length} unit");

                    foreach (GameObject obj in units)
                    {
                        Unit unit = obj.GetComponent<Unit>();
                        unit.OnRoute(transform.position);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            Unit unit = other.GetComponent<Unit>();
            Debug.Log($"đã tới nơi, xe tới là unit {unit.unitType}");
            if (unit != null && unit.unitType == unitType)
            {
                unit.OnScene();
                timeResolve = Status.GetTimeResolve(unit.vehicleType);
                Debug.Log($"time solve = {timeResolve}");
                StopCoroutine(accidentCoroutine);
                accidentCoroutine = StartCoroutine(MissionSuccess());
            }
            else
            {
                Debug.Log("gửi nhầm xe");
            }
        }
        
    }
    
    IEnumerator MissionSuccess()
    {
        Debug.Log("đã bắt đầu được giải quyết");
        yield return new WaitForSeconds(timeResolve);
        point.PlusPoint();
        Debug.Log($"cash: {point.cash}, reputation: {point.reputation}, resolved: {point.resolved}");
        Debug.Log("giải quyết thành công");
        Destroy(gameObject);
        yield return null;
    }

    IEnumerator MissionFail()
    {
        Debug.Log("hãy mau xử lý!");
        yield return new WaitForSeconds(10);
        Debug.Log("đã không xử lý được tai nạn");
        // accidentManager.noti.SetActive(false);
        point.MinusPoint();
        Destroy(gameObject);
        yield return null;
    }
}

using UnityEngine;
using TMPro;

public class Summary : MonoBehaviour
{
    public PointManager point;
    public GameObject endDay;
    public TextMeshProUGUI day;
    public TextMeshProUGUI cashSum;
    public TextMeshProUGUI reputationSum;
    public TextMeshProUGUI resolvedSum;
    public TextMeshProUGUI unresolve;

    // void Awake()
    // {
    //     timer = GetComponent<Timer>();
    // }
    void Update()
    {
        day.SetText(point.day.ToString());
        cashSum.SetText(point.cash.ToString());
        reputationSum.SetText(point.reputation.ToString());
        resolvedSum.SetText(point.resolved.ToString());
        unresolve.SetText(point.unresolve.ToString());
    }

    public void NextDay()
    {
        endDay.SetActive(false);
        Time.timeScale = 1;
    }
}

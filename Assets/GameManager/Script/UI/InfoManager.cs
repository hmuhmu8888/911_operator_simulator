using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoManager : MonoBehaviour
{
    public PointManager pointManager;
    public TextMeshProUGUI cash;
    public TextMeshProUGUI reputation;
    public TextMeshProUGUI resolved;
    public TextMeshProUGUI unresolve;

    void Update()
    {
        cash.SetText(pointManager.cash.ToString());
        reputation.SetText(pointManager.reputation.ToString());
        resolved.SetText(pointManager.resolved.ToString());
        unresolve.SetText(pointManager.unresolve.ToString());
    }
}

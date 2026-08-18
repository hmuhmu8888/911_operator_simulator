using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime = 840f;
    public PointManager point;
    public GameObject endDay;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 5)
        {
            timerText.color = Color.red;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            GameStop();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    IEnumerator GameStop()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(2f);
        point.day++;
        endDay.SetActive(true);
        remainingTime = 840f;
        timerText.color = Color.white;
        yield return null;
    }
}

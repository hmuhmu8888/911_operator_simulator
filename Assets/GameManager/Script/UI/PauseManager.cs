using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject setting;
    void Start()
    {
        Debug.Log(pauseMenu);
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Debug.Log("tình trạng sau ấn pause: " + pauseMenu.activeSelf);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Debug.Log("tình trạng sau ấn resume: " + pauseMenu.activeSelf);
        Time.timeScale = 1;
    }
    public void Home()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("menu");
    }
    public void Save()
    {
        Debug.Log("đã lưu game nhưng chưa thực sự lưu =))))");
    }
    public void SettingOpen()
    {
        setting.SetActive(true);
        Debug.Log("tình trạng sau ấn mở setting: " + setting.activeSelf);
    }
    public void SettingClose()
    {
        setting.SetActive(false);
        Debug.Log("tình trạng sau ấn đóng setting: " + setting.activeSelf);
    }
}

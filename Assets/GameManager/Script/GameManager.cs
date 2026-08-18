using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    private void Awake()
    {
        if (ins == null)
        {
            ins=this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadChooseMapScene()
    {
        SceneManager.LoadScene("chooseMap");
    }
    public void LoadSavedPlayMap()
    {
        SceneManager.LoadScene("playMap");
        // DataPersistanceManager.instance.LoadGame();
        Time.timeScale = 1;
    }
    public void LoadNewPlayMap()
    {
        SceneManager.LoadScene("playMap");
        // DataPersistanceManager.instance.NewGame();
        Time.timeScale = 1;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("menu");
    }
    public void LoadSetting()
    {
        SceneManager.LoadScene("setting");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
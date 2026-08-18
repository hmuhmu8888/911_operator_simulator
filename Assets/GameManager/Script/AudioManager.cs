using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header ("Audio Source")]
    // Tạo biến lưu trữ các AudioSource
    public AudioSource menuBGM_Source;
    public AudioSource SFX_Source;

    [Header ("Audio Clip")]
    // Tạo biến lưu trữ các AudioClip
    public AudioClip menuBGM;
    public AudioClip mapBGM;
    public AudioClip clickButton;
    public AudioClip hoverButton;
    public AudioClip clickText;
    public AudioClip runText;

    [Header ("Audio Mixer")]
    [SerializeField] private AudioMixer myAudio;

    void Start()
    {
        menuBGM_Source.clip=menuBGM;
        menuBGM_Source.Play();
        myAudio.SetFloat("menuBGM",Mathf.Log10(PlayerPrefs.GetFloat("menuBGMVol"))*20);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += InPlay;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= InPlay;
    }

    private void InPlay(Scene scene, LoadSceneMode mode)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "playMap")
        {
            menuBGM_Source.clip=mapBGM;
            menuBGM_Source.Play();
        }
        if (sceneName == "menu" && menuBGM_Source.clip==mapBGM)
        {
            menuBGM_Source.clip=menuBGM;
            menuBGM_Source.Play();
        }
    }

    public void PlaySFX(AudioClip sfx)
    {
        SFX_Source.clip=sfx;
        SFX_Source.PlayOneShot(sfx);
    }
}

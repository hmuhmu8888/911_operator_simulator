using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
   [SerializeField] private AudioMixer myAudio;
   [SerializeField] private Slider menuBGM_Slider;
   [SerializeField] private Slider sfx_Slider;

   private void Start()
   {
        if (PlayerPrefs.HasKey("menuBGMVol"))
        {
            LoadMenuBgmVol();
        }
        else
        {
            SetMenuBgmVol();
            SetSfxVol();
        }
   }

   public void SetMenuBgmVol()
    {
        float menuVol = menuBGM_Slider.value;
        myAudio.SetFloat("menuBGM",Mathf.Log10(menuVol)*20); //Mathf.Log10(vol)*20
        PlayerPrefs.SetFloat("menuBGMVol",menuVol);
    }

    public void SetSfxVol()
    {
        float sfxVol = sfx_Slider.value;
        myAudio.SetFloat("sfx",Mathf.Log10(sfxVol)*20); //Mathf.Log10(vol)*20
        PlayerPrefs.SetFloat("sfxVol",sfxVol);
    }

    public void LoadMenuBgmVol()
    {
        menuBGM_Slider.value=PlayerPrefs.GetFloat("menuBGMVol");
        sfx_Slider.value=PlayerPrefs.GetFloat("sfxVol");
        SetMenuBgmVol();
        SetSfxVol();
    }
}

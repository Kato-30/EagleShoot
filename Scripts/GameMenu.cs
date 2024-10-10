using System.Collections;
using System.Collections.Generic;
using GamePix;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public TextMeshProUGUI highestScore;
    public TextMeshProUGUI coin;
    public GameObject upgrade;
    public GameObject infor;
    public GameObject volumeSetting;
    public AudioMixer mixer;
    public Slider musicSlider;
    private bool isShowInfor = false;
    private bool isShowVolumeSetting = false;

    void Start()
    {
        //ads gamepics
        Gpx.Ads.InterstitialAd(OnInterstitalAdSuccess);

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }

    //ads gamepics
    [AOT.MonoPInvokeCallback(typeof(Gpx.gpxCallback))]
    public static void OnInterstitalAdSuccess()
    {
        Gpx.Log("SUCCESS");
    }
    //

    void Update()
    {
        highestScore.SetText(PlayerPrefs.GetInt("highScore").ToString());
        coin.SetText(PlayerPrefs.GetInt("Coin").ToString());
    }

    public void Game()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenUpgrade(bool c)
    {
        upgrade.SetActive(c);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowInfor()
    {
        isShowInfor = !isShowInfor;
        infor.SetActive(isShowInfor);
    }

    public void ShowVolumeSetting()
    {
        isShowVolumeSetting = !isShowVolumeSetting;
        volumeSetting.SetActive(isShowVolumeSetting);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }
}

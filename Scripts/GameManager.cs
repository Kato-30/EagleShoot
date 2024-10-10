using System.Collections;
using System.Collections.Generic;
using GamePix;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score = 0;
    private int highScore = 0;
    private int coin = 0;
    public int bullet = 30;
    private bool isGameOver = false;
    private int eagleEscapeCount = 0;
    private int maxEagleEscape = 5;
    public Image[] eagleIcons;
    public GameObject gameOverScreen;

    public GameObject ammoObj;

    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private bool isShowVolumeSetting = false;
    public GameObject volumeSetting;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        highScore = PlayerPrefs.GetInt("highScore", 0);
        coin = PlayerPrefs.GetInt("Coin", 0);
        bullet = PlayerPrefs.GetInt("Ammo", 30);
        ResetEagleIcons();
        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSfxVolume();
        }
    }

    void Update()
    {
        if (isGameOver)
        {
            Lose();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            bullet = PlayerPrefs.GetInt("Ammo");
        }
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddCoin(int coinValue)
    {
        coin += coinValue;
        PlayerPrefs.SetInt("Coin", coin);
    }

    public int GetCoin()
    {
        return coin;
    }

    public void UpdateBullet()
    {
        bullet--;
    }

    public int GetBullet()
    {
        return bullet;
    }

    public void GameOver()
    {
        isGameOver = true;
        //ads gamepics
        Gpx.Ads.InterstitialAd(OnInterstitalAdSuccess);
    }

    //ads gamepics
    [AOT.MonoPInvokeCallback(typeof(Gpx.gpxCallback))]
    public static void OnInterstitalAdSuccess()
    {
        Gpx.Log("SUCCESS");
    }
    //

    public void EagleEscape()
    {
        if (eagleEscapeCount < maxEagleEscape)
        {
            eagleIcons[eagleEscapeCount].enabled = false;
        }
        eagleEscapeCount++;
        Debug.Log(eagleEscapeCount);
        if (eagleEscapeCount >= maxEagleEscape)
        {
            GameOver();
        }
    }

    void ResetEagleIcons()
    {
        foreach (Image icon in eagleIcons)
        {
            icon.enabled = true;
        }
    }

    public void Lose()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        AudioManager.instance.sfxSource.Stop();
        DisCursor();
    }

    public void Ammo(bool c)
    {
        ammoObj.SetActive(c);
    }

    public void SetCursor(Texture2D customCursor)
    {
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
    }

    public void DisCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        mixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
        SetSfxVolume();
    }
}

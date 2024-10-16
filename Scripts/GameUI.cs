using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI highestScore;
    public TextMeshProUGUI coin;
    public Texture2D customCursor;
    public TextMeshProUGUI bullet;

    void Start()
    {
        coin.SetText(PlayerPrefs.GetInt("Coin").ToString());
        bullet.SetText(PlayerPrefs.GetInt("Ammo").ToString());

        if (customCursor != null)
        {
            GameManager.Instance.SetCursor(customCursor);
        }
    }

    void Update()
    {
        score.SetText(GameManager.Instance.GetScore().ToString());
        currentScore.SetText(GameManager.Instance.GetScore().ToString());
        highestScore.SetText(PlayerPrefs.GetInt("highScore").ToString());
        coin.SetText(GameManager.Instance.GetCoin().ToString());
        bullet.SetText(GameManager.Instance.GetBullet().ToString());
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}

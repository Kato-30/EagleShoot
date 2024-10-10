using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Image[] lvlAmmu;
    public Image[] lvlRateOfFire;
    public Image[] lvlBulletSpeed;
    public TextMeshProUGUI txtAmmu;
    public TextMeshProUGUI txtRateOF;
    public TextMeshProUGUI txtBulletSpeed;
    public string[] txtPrice;
    private int coin;
    private int ammuCount = 0;
    private int rateOfFireCount = 0;
    private int bulletSpeedCount = 0;
    private int upgradeAmmo;
    private float upgradeRate;
    private float upgradeSpeed;

    private void Start()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);

        upgradeAmmo = PlayerPrefs.GetInt("Ammo", 30);
        upgradeRate = PlayerPrefs.GetFloat("FireRate", 0.8f);
        upgradeSpeed = PlayerPrefs.GetFloat("BulletSpeed", 10f);

        txtAmmu.SetText(txtPrice[0]);
        txtRateOF.SetText(txtPrice[0]);
        txtBulletSpeed.SetText(txtPrice[0]);

        ammuCount = PlayerPrefs.GetInt("AmmuC", 0);
        rateOfFireCount = PlayerPrefs.GetInt("RateOFC", 0);
        bulletSpeedCount = PlayerPrefs.GetInt("BulletSpeedC", 0);
    }

    private void Update()
    {
        FillLvl(ammuCount, lvlAmmu, txtAmmu);
        FillLvl(rateOfFireCount, lvlRateOfFire, txtRateOF);
        FillLvl(bulletSpeedCount, lvlBulletSpeed, txtBulletSpeed);
    }

    public void UpgradeAmmu()
    {
        if (ammuCount < lvlAmmu.Length)
        {
            int price = int.Parse(txtPrice[ammuCount]);
            if (coin >= price)
            {
                coin -= price;
                PlayerPrefs.SetInt("Coin", coin);

                upgradeAmmo += 2;
                PlayerPrefs.SetInt("Ammo", upgradeAmmo);

                lvlAmmu[ammuCount].color = Color.green;
                txtAmmu.SetText(txtPrice[ammuCount + 1]);

                ammuCount++;
                PlayerPrefs.SetInt("AmmuC", ammuCount);
            }
            else
            {
                Debug.Log("Ko đủ điều kiện nâng cấp !");
            }
        }
    }

    public void UpgradeRateOF()
    {
        if (rateOfFireCount < lvlRateOfFire.Length)
        {
            int price = int.Parse(txtPrice[rateOfFireCount]);
            if (coin >= price)
            {
                coin -= price;
                PlayerPrefs.SetInt("Coin", coin);

                upgradeRate -= 0.1f;
                PlayerPrefs.SetFloat("FireRate", upgradeRate);

                lvlRateOfFire[rateOfFireCount].color = Color.green;
                txtRateOF.SetText(txtPrice[rateOfFireCount + 1]);

                rateOfFireCount++;
                PlayerPrefs.SetInt("RateOFC", rateOfFireCount);
            }
            else
            {
                Debug.Log("Ko đủ điều kiện nâng cấp !");
            }
        }
    }

    public void UpgradeBulletSpeed()
    {
        if (bulletSpeedCount < lvlBulletSpeed.Length)
        {
            int price = int.Parse(txtPrice[bulletSpeedCount]);
            if (coin >= price)
            {
                coin -= price;
                PlayerPrefs.SetInt("Coin", coin);

                upgradeSpeed += 1.5f;
                PlayerPrefs.SetFloat("BulletSpeed", upgradeSpeed);

                lvlBulletSpeed[bulletSpeedCount].color = Color.green;
                txtBulletSpeed.SetText(txtPrice[bulletSpeedCount + 1]);

                bulletSpeedCount++;
                PlayerPrefs.SetInt("BulletSpeedC", bulletSpeedCount);
            }
            else
            {
                Debug.Log("Ko đủ điều kiện nâng cấp !");
            }
        }
    }

    private void FillLvl(int n, Image[] images, TextMeshProUGUI text)
    {
        for (int i = 0; i < n; i++)
        {
            images[i].color = Color.green;
            text.SetText(txtPrice[i + 1]);
        }
    }
}

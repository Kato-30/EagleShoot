using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public Transform firePoint;
    public float fireRate = 0.8f;
    private float nextFireTime;
    public int maxAmmo = 30;
    private int currentAmmo;

    void Start()
    {
        maxAmmo = PlayerPrefs.GetInt("Ammo", 30);
        currentAmmo = maxAmmo;

        fireRate = PlayerPrefs.GetFloat("FireRate", 0.8f);
        if (fireRate < 0.3f)
        {
            fireRate = 0.3f;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        RotateGun();

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadAmmo();
        }
    }

    void Shoot()
    {
        if (bulletPrefabs != null)
        {
            Instantiate(bulletPrefabs, firePoint.position, firePoint.rotation);
            AudioManager.Instance.PlayShootSound();
            GameManager.Instance.UpdateBullet();
            currentAmmo--;
            if (currentAmmo <= 0)
            {
                GameManager.Instance.Ammo(true);
                Debug.Log("Hết đạn !!!");
            }
        }
    }

    void ReloadAmmo()
    {
        currentAmmo = maxAmmo;
        AudioManager.Instance.PlayReloadSound();
        GameManager.Instance.Ammo(false);
        PlayerPrefs.SetInt("Ammo", currentAmmo);
        Debug.Log("Đã nạp đạn !!!");
    }

    void RotateGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}

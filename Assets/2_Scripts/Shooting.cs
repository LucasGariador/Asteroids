using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform shootingPoint;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float bulletSpeed;

    private float timer = 0;
    private bool canShot;

    private void Start()
    {
        timer = cooldown;
    }
    private void Update()
    {
        if (!canShot && timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            canShot = true;
        }
    }

    public void Shot()
    {
        if (canShot)
        {
            var bullet = ObjectsPooling.Instance.bulletsPool.Get();
            bullet.transform.SetPositionAndRotation(shootingPoint.position, shootingPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed);

            timer = cooldown;
            canShot = false;
        }
    }


}

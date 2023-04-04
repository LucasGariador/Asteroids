using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float lifespan;

    private float timer;
    private void OnEnable()
    {
        timer = lifespan;
    }

    private void Update()
    {
        if (timer < 0)
            DisableBullet();
        else
            timer -= Time.deltaTime;
    }

    public void DisableBullet()
    {
        ObjectsPooling.Instance.BulletsPool.Release(gameObject);
    }
}

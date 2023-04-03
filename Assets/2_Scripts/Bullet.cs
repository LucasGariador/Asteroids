using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float lifespan;
    private void OnEnable()
    {
        Invoke(nameof(DisableBullet), lifespan);
    }

    public void DisableBullet()
    {
        ObjectsPooling.Instance.DisableBullet(gameObject);
    }
}

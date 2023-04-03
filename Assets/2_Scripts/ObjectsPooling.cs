using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsPooling : MonoBehaviour
{
    public static ObjectsPooling Instance { get; private set; }
    public ObjectPool<GameObject> bulletsPool { get; private set; }
    public GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletsParent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bulletsPool = new ObjectPool<GameObject>(()=>
        {
            return Instantiate(bulletPrefab, bulletsParent);
        },bullet =>
        {
            bullet.gameObject.SetActive(true);
        }, bullet =>
        {
            bullet.SetActive(false);
        }, bullet =>
        {
            Destroy(bullet);
        },false,50,100);

    }

    public void DisableBullet(GameObject bullet)
    {
        bulletsPool.Release(bullet);
    }

}

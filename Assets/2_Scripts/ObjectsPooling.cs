using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsPooling : MonoBehaviour
{
    public static ObjectsPooling Instance { get; private set; }
    public ObjectPool<GameObject> BulletsPool { get; private set; }
    public ObjectPool<GameObject> BigAsteroidPool { get; private set; }
    public ObjectPool<GameObject> SmallAsteroidPool { get; private set; }

    [Header("Bullets")]
    public GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletsParent;

    [Header("Asteroids")]
    public GameObject bigAsteroidPrefab;
    public GameObject smallAsteroidPrefab;

    public Transform asteroidsParent;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BulletsPool = new ObjectPool<GameObject>(()=>
        {
            return Instantiate(bulletPrefab, bulletsParent);
        },bullet =>
        {
            bullet.SetActive(true);
        }, bullet =>
        {
            bullet.SetActive(false);
        }, bullet =>
        {
            Destroy(bullet);
        },false,50,100);

        BigAsteroidPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(bigAsteroidPrefab, asteroidsParent);
        }, bigAsteroid =>
        {
            bigAsteroid.SetActive(true);
        }, bigAsteroid =>
        {
            bigAsteroid.SetActive(false);
        }, bigAsteroid =>
        {
            Destroy(bigAsteroid);
        }, false, 10, 20);

        SmallAsteroidPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(smallAsteroidPrefab, asteroidsParent);
        }, smallAsteroid =>
        {
            smallAsteroid.SetActive(true);
        }, smallAsteroid =>
        {
            smallAsteroid.SetActive(false);
        }, smallAsteroid =>
        {
            Destroy(smallAsteroid);
        }, false, 10, 20);
    }

    public void DisableBullet(GameObject bullet)
    {
        BulletsPool.Release(bullet);
    }

    public void DisableBigAsteroid(GameObject asteroid)
    {
        BigAsteroidPool.Release(asteroid);
    }

    public void DisableSmallAsteroid(GameObject asteroid)
    {
        SmallAsteroidPool.Release(asteroid);
    }

}

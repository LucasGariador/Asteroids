using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float lifespan;
    private float bulletTimer;

    private void OnEnable()
    {
        bulletTimer = lifespan;
    }

    private void Update()
    {
        if (bulletTimer < 0)
            DisableBullet();
        else
            bulletTimer -= Time.deltaTime;
    }

    public void DisableBullet() => ObjectsPooling.Instance.BulletsPool.Release(gameObject);
}

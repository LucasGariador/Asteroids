using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField, Tooltip("Position and direction to shot")]
    private Transform shootingPoint;
    [SerializeField, Tooltip("Time between shots")]
    private float cooldown;
    [SerializeField, Tooltip("Starting speed of the bullet")]
    private float bulletSpeed;

    private float timer = 0;
    private bool canShot;

    private void Start()
    {
        timer = cooldown;
    }
    private void Update()
    {
        RunTimer();
    }

    private void RunTimer()
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
            var bullet = ObjectsPooling.Instance.BulletsPool.Get();
            bullet.transform.SetPositionAndRotation(shootingPoint.position, shootingPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed);
            bullet.GetComponent<Rigidbody2D>().gravityScale = 0f;

            timer = cooldown;
            canShot = false;
        }
    }


}

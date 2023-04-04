using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private bool isBigAsteroid;
    [SerializeField]
    private float minStartVel;
    [SerializeField]
    private float maxStartVel;
    private Rigidbody2D rb;

    private void OnEnable()
    {
        transform.rotation = Random.rotation;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
         
        rb.AddForce(transform.up * Random.Range(minStartVel, maxStartVel));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            ObjectsPooling ObjPooling = ObjectsPooling.Instance;
            ObjPooling.BulletsPool.Release(collision.gameObject);
            if (isBigAsteroid)
            {
                ObjPooling.BigAsteroidPool.Release(gameObject);
                GameManager.Instance._score += 50;
                var minAsteroid1 = ObjPooling.SmallAsteroidPool.Get();
                var minAsteroid2 = ObjPooling.SmallAsteroidPool.Get();

                minAsteroid1.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
                minAsteroid2.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            }
            else
            {
                ObjPooling.SmallAsteroidPool.Release(gameObject);
                GameManager.Instance._score += 100;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}

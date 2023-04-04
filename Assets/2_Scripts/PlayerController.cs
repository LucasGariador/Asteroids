using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Rotation Speed multiplier")]
    private float rotSpeed;

    [SerializeField, Tooltip("Acceleration Force")]
    private float speedForce;
    [SerializeField, Tooltip("Aceleration Speed")]
    private float acceleration = 0.2f;

    [SerializeField, Tooltip("Max Acceleration")]
    private float maxSpeed;


    private Rigidbody2D rb;
    private Shooting shooting;

    private Vector2 currentDirection;
    private int rotDirection;
    private bool startPushing;
    private bool isPushing;
    private float currentPushForce = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooting = GetComponent<Shooting>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        GetInput();

        ResetOnStartPushing();
    }

    private void ResetOnStartPushing()
    {
        if (startPushing)
        {
            currentDirection = transform.up;
            currentPushForce = 0f;
        }
    }

    private void FixedUpdate()
    {
        SetMovement();
    }

    private void GetInput()
    {
        //Rotation Input
        rotDirection = (int)Input.GetAxisRaw("Horizontal");
        //Movement Input
        startPushing = Input.GetKeyDown(KeyCode.W);
        isPushing = Input.GetKey(KeyCode.W);
        //Shooting Input
        if (Input.GetMouseButtonDown(0)) { shooting.Shot(); }
    }

    private void SetMovement()
    {
        //Rotation
        transform.Rotate(0f, 0f, -rotDirection * rotSpeed);

        //Movement
        if(isPushing)
        {
            currentPushForce = Mathf.Clamp(currentPushForce += speedForce, 0f, maxSpeed);
            rb.AddForce(acceleration * currentPushForce * currentDirection, ForceMode2D.Impulse);
        }
    }
}

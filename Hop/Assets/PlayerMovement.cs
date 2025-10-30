using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce;
    public float speed;

    [Space]
    public float fallMultiplier;
    public float lowJumpMultiplier;

    bool left;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Better Jumping
        if(rb.linearVelocityY > 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(rb.linearVelocityY > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Jump
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void Movement()
    {
        if (left)
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
        }else
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            left = !left;
        }
    }
}

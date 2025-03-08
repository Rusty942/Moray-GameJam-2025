using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float thrustForce = 5f;
    public float maxSpeed = 10f;
    public float damping = 0.99f;

    private Rigidbody2D rb;
    private bool facingRight = true;
    
    public AudioSource engineSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.drag = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleEngineSound();
    }

    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        rb.AddForce(Vector2.up * verticalInput * thrustForce);
        
        rb.AddForce(Vector2.right * horizontalInput * thrustForce);
        
        if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
        else if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        
        rb.velocity *= damping;
        
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    
    void HandleEngineSound()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            if (!engineSound.isPlaying)
            {
                engineSound.Play();
            }
        }
        else
        {
            if (engineSound.isPlaying)
            {
                engineSound.Stop();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
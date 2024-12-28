using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float moveDirection;
    public float direction = 1;
    public float moveSpeed = 4;
    public bool isGrounded;
    public Vector2 velocity;
    [SerializeField] private float gravity;
    [SerializeField] private Transform groundCheck1;
    [SerializeField] private Transform groundCheck2;
    public LayerMask groundLayer;
    [SerializeField] private float jumpHeight;
    [SerializeField] private bool isJumping;



    void Update()
    {
        velocity.x = moveDirection * moveSpeed;
        
        if (Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, groundLayer) && !isJumping && rb.velocity.y < 0.2f)
        { isGrounded = true; }
        else { isGrounded = false; }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) { Jump(); }
        else if (isGrounded && rb.velocity.y < 0.2f)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime * 60;
        }

        if (isGrounded) { moveDirection = Input.GetAxisRaw("Horizontal"); }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
        }
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void FixedUpdate()
    {    
        rb.velocity = velocity; //* Time.fixedDeltaTime * 60;
    }

    private void Jump()
    {
        velocity.y = jumpHeight;
        isGrounded = false;
        isJumping = true;
        Invoke("ResetJump", 0.1f);
    }

    private void ResetJump()
    {
        isJumping = false;
    }
}

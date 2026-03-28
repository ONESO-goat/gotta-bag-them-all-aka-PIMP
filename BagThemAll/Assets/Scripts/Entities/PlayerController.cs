using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Read input every frame
        float moveX = Input.GetAxisRaw("Horizontal"); // left/right arrow or A/D
        float moveY = Input.GetAxisRaw("Vertical");   // up/down arrow or W/S

        movement = new Vector2(moveX, moveY).normalized;
        // .normalized makes diagonal movement the same speed as straight movement
    }

    void FixedUpdate()
    {
        // Physics movement goes in FixedUpdate, not Update
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
// WASDCamera.cs (2D)
// Simple 2D camera controller for Unity. Attach to a Camera GameObject.
// Movement: WASD for XY pan, Shift to sprint. Optional smooth rotation to face a target (Z rotation).

using UnityEngine;

public class Movement
{
    // Movement
    public float moveSpeed = 5f; // units per second
    public float sprintMultiplier = 2f;
    public bool allowMovement;
    private Rigidbody2D rbody;
    private Vector2 vel = Vector2.zero;
    // Centering options (rotate to face target on Z axis)
    public Transform target = null;
    public bool centerTarget = false;


    void Start()
    {
        allowMovement = false;
    }

    public void SetBody(Rigidbody2D rigidbody)
    {
        rbody = rigidbody;
    }

    void Update()
    {
        if (allowMovement)
        {
            vel.x = Input.GetAxisRaw("Horizontal");
            vel.y = Input.GetAxisRaw("Vertical");

            vel.Normalize();
        }
    }

    void FixedUpdate()
    {
        rbody.velocity = vel * moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            rbody.velocity *= sprintMultiplier;
        }
    }

    void OnBecameVisible()
    {
        allowMovement = true;
    }

    void OnBecameInvisible()
    {
        allowMovement = false;
    }
}

// WASDCamera.cs (2D)
// Simple 2D camera controller for Unity. Attach to a Camera GameObject.
// Movement: WASD for XY pan, Shift to sprint. Optional smooth rotation to face a target (Z rotation).

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Movement : MonoBehaviour
{
    // Movement
    public float moveSpeed = 5f; // units per second
    public float sprintMultiplier = 2f;
    public bool allowMovement;
    private Rigidbody2D rbody;
    private Vector2 vel = Vector2.zero;
    private InteractionDetection detector = null;


    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        detector = new InteractionDetection();
        allowMovement = false;
    }

    private void GetMovementInput()
    {
        vel.x = Input.GetAxisRaw("Horizontal");
        vel.y = Input.GetAxisRaw("Vertical");

        vel.Normalize();
    }

    public void freezeMovement()
    {
        allowMovement = false;
        rbody.velocity = Vector2.zero;
    }

    public void unfreezeMovement()
    {
        allowMovement = true;
    }

    public void SetBody(Rigidbody2D rigidbody)
    {
        rbody = rigidbody;
    }

    void Update()
    {
        if (allowMovement)
        {
            GetMovementInput();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            detector.isInteracting = true;
        } else if (Input.GetKeyUp(KeyCode.E) && detector.isInteracting)
        {
            detector.isInteracting = false;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // This method is intentionally left empty to ensure that the Rigidbody2D's collision detection is active.
        // The actual interaction logic is handled in the InteractionDetection class via OnCollisionStay2D and OnCollisionExit2D.
        detector.OnCollisionEnter2D(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // detector.OnCollisionStay2D(collision);
        detector.CollisionCheck(collision.gameObject);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        detector.OnCollisionExit2D(collision);
    }

    void OnBecameVisible()
    {
        unfreezeMovement();
    }

    void OnBecameInvisible()
    {
        freezeMovement();
    }

}

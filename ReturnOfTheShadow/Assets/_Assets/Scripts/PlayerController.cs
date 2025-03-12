using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private Rigidbody2D rb;
    
    [Title("Move")]
    [SerializeField, GUIColor("RGB(0, 0.5, 0)")] private float movementSpeed = 5f;
    [SerializeField, GUIColor("RGB(0, 0.5, 0)")] private float jumpPower = 5f;
    
    [Title("Gravity")]
    [SerializeField, GUIColor("RGB(0, 0.5, 0)")] private float baseGravity = 2f;
    [SerializeField, GUIColor("RGB(0, 0.5, 0)")] private float maxFallSpeed = 18f;
    [SerializeField, GUIColor("RGB(0, 0.5, 0)")] private float fallSpeedMultiplier = 2f;
    [Title("GroundCheck")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    [SerializeField] private LayerMask groundLayer;
    private int maxJumps = 1;
    [SerializeField, GUIColor("Yellow"),ReadOnly]private int jumpRemaining;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateGroundCheck();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * movementSpeed, rb.linearVelocity.y);
        FixedUpdateGravity();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void FixedUpdateGravity()
    {
        if (rb.linearVelocity.y < 0f)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y - maxFallSpeed));
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && jumpRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            jumpRemaining--;
        }
        else if (context.canceled) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private void UpdateGroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer))
        {
            jumpRemaining = maxJumps;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
public class PlayerController : MonoBehaviour
{
    private float horizontal;
    [SerializeField, GUIColor("RGB(0, 0.5, 0)")] private float movementSpeed = 5f;
    [SerializeField, GUIColor("RGB(0, 0.5, 0)")] private float jumpPower = 5f;
    private Rigidbody2D rb;
    
    [Title("Gravity")]
    [SerializeField, GUIColor("RGB(0, 0.5, 0)")] private float baseGravity = 2f;
    [SerializeField, GUIColor("RGB(0, 0.5, 0)")] private float maxFallSpeed = 18f;
    [SerializeField, GUIColor("RGB(0, 0.5, 0)")] private float fallSpeedMultiplier = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * movementSpeed, rb.linearVelocity.y);
        Gravity();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void Gravity()
    {
        if (rb.linearVelocity.y < 0f)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y - maxFallSpeed));
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }
        else if (context.canceled)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }
}

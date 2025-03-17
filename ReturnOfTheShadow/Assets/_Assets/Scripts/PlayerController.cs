using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    [FormerlySerializedAs("movementSpeed")]
    [Title("Move")]
    [SerializeField, GUIColor("Yellow"), ReadOnly] private float movementSpeedFinal;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float midAirSpeed = 2.5f;
    [SerializeField] private float jumpPower = 5f;
    [Title("Gravity")]
    [SerializeField] private float baseGravity = 2f;
    [SerializeField] private float maxFallSpeed = 18f;
    [SerializeField] private float fallSpeedMultiplier = 2f;
    [Title("GroundCheck")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    [SerializeField] private LayerMask groundLayer;
    private int maxJumps = 1;
    [SerializeField, GUIColor("Yellow"),ReadOnly]private int jumpRemaining;
    [Title("Object Pool")]
    [SerializeField] private ObjectPool objectPool;
    private GameObject poolObject;
    [Title("Crosshair")]
    [SerializeField] private Transform crosshair;
    [SerializeField] private float lookSensitivity = 5f;
    private float lookHorizontal;
    private float lookVertical;
    [SerializeField, GUIColor("Yellow"),ReadOnly] private float crosshairDistanceFromPlayer;
    [SerializeField, GUIColor("Green")] private float maxDistance;

    #region TestMethods
    [Button]
    public void TestSpawn()
    {
        poolObject = objectPool.GetObject();
        poolObject.transform.position = gameObject.transform.position;
        poolObject.SetActive(true);
    }

    [Button]
    public void TestDespawn()
    {
        objectPool.ReturnObject(poolObject);
    }
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectPool.objectPool.Clear();
        objectPool.LoadObjectPool();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        UpdateGroundCheck();
        crosshairDistanceFromPlayer = Vector2.Distance(transform.position, crosshair.position);
        if (playerInput.currentControlScheme == "Gamepad")
        {
            UpdateMoveCrosshair();
        }
        else
        {
            UpdateOnLookMouse();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * movementSpeedFinal, rb.linearVelocity.y);
        FixedUpdateGravity();
    }

    

    private void FixedUpdateGravity()
    {
        if (rb.linearVelocity.y < maxDistance)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y - maxFallSpeed));
        }
    }

    private void UpdateMoveCrosshair()
    {
        Vector3 newPosition = crosshair.position + new Vector3(lookHorizontal * lookSensitivity, lookVertical * lookSensitivity, 0);
        
        float distance = Vector3.Distance(gameObject.transform.position, newPosition);

        // Clamp the crosshair's position if it exceeds the max distance
        if (distance > maxDistance)
        {
            Vector3 normalizedDirection = (newPosition - gameObject.transform.position).normalized; // Get direction from player to crosshair
            newPosition = gameObject.transform.position + normalizedDirection * maxDistance; // Set position at max distance
        }
        
        crosshair.position = newPosition;
    }
    #region Movement Methods
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
    public void OnMove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void UpdateGroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer))
        {
            jumpRemaining = maxJumps;
            movementSpeedFinal = movementSpeed;
        }
        else
        {
            movementSpeedFinal = midAirSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
    }
    #endregion
    public void OnLookController(InputAction.CallbackContext context)
    {
        lookHorizontal = context.ReadValue<Vector2>().x;
        lookVertical = context.ReadValue<Vector2>().y;
    }

    private void UpdateOnLookMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        crosshair.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
    }
}

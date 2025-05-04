using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    [Title("Move")]
    [SerializeField, GUIColor("Yellow"), ReadOnly] private float movementSpeedFinal;
    [SerializeField] private VariableReference<float> movementSpeed;
    [SerializeField] private float midAirSpeed;
    [SerializeField] private float defaultJumpPower;
    [SerializeField] private float midAirJumpPower;
    //CoyoteTime
    private float timeOffFloor;
    private float maxCoyoteTime = 0.15f;
    //CoyoteTime
    private float jumpPower;
    [Title("Gravity")]
    [SerializeField] private float baseGravity = 2f;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float fallSpeedMultiplier;
    [Title("GroundCheck")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    [SerializeField] private LayerMask groundLayer;
    private int maxJumps = 2;
    [SerializeField] private VariableReference<int> jumpRemaining;
    [Title("Object Pool")]
    [SerializeField] private ObjectPool objectPool;
    private GameObject poolObject;
    [SerializeField] private Transform boomerangSpawn;
    [Title("Crosshair")]
    [SerializeField] private Transform crosshair;
    [SerializeField] private float lookSensitivity = 5f;
    private float lookHorizontal;
    private float lookVertical;
    [SerializeField] private VariableReference<Vector2> crosshairLocation;
    [SerializeField, GUIColor("Green")] private float maxDistance;
    [Title("DropShadow")]
    [SerializeField] private Transform dropShadow;
    private LineRenderer lineRenderer;
    [Title("Abilities")]
    [SerializeField] private AbilityHolder ability;
    private float delayAbility;
    [SerializeField] private VariableReference<Vector2> boomerangLocation;
    
    //Collider Event Trigger
    private ColliderEventTrigger eventTrigger;
    
    
    #region Boomerang Spawn Stuff
    public void LoadBoomerang()
    {
        poolObject = objectPool.GetObject();
    }

    private void SpawnBoomerang()
    {
        poolObject.transform.position = boomerangSpawn.position;
        poolObject.SetActive(true);
    }
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectPool.objectPool.Clear();
        objectPool.LoadObjectPool();
        playerInput = GetComponent<PlayerInput>();
        eventTrigger = GetComponent<ColliderEventTrigger>();
        eventTrigger.onCollisionEnterBoomerang.AddListener(ReturnBoomerangOnCollision);
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnDisable()
    {
        eventTrigger.onCollisionEnterBoomerang.RemoveListener(ReturnBoomerangOnCollision);
    }

    private void Update()
    {
        UpdateDropShadow();
        UpdateGroundCheck();
        crosshairLocation.value = crosshair.position;
        lineRenderer.SetPosition(0,boomerangSpawn.position);
        lineRenderer.SetPosition(1,crosshair.position);
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

    private void UpdateDropShadow()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 20, groundLayer);
        if (hit.collider)
        {
            dropShadow.position = hit.point;
        }
    }

    private void FixedUpdateGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y - maxFallSpeed));
        }
        else
        {
            rb.gravityScale = 3;
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
            newPosition = gameObject.transform.position + (normalizedDirection * maxDistance); // Set position at max distance
        }
        crosshair.position = newPosition;
    }
    #region Movement Methods
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (timeOffFloor <= maxCoyoteTime || jumpRemaining > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            }
        }
        else if (context.canceled) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            jumpRemaining.value--;
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
            jumpRemaining.value = maxJumps;
            movementSpeedFinal = movementSpeed;
            jumpPower = defaultJumpPower;
            timeOffFloor = 0f;
            dropShadow.gameObject.SetActive(false);
        }
        else
        {
            movementSpeedFinal = midAirSpeed;
            jumpPower = midAirJumpPower;
            timeOffFloor += Time.deltaTime;
            dropShadow.gameObject.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
    }
    #endregion
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LoadBoomerang();
            if (poolObject != null)
            {
                SpawnBoomerang();
            }
        }
    }

    public void OnTeleport(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(Teleport());
        }
    }
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
    
    private void ReturnBoomerangOnCollision(GameObject other)
    {
        IReturn returnObject = other.gameObject.GetComponent<IReturn>();
        returnObject.DespawnBoomerang();
    }
    private IEnumerator Teleport()
    {
        foreach (Ability i in ability.abilities)
        {
            if (i is TeleportAbility teleportAb)
            {
               delayAbility = teleportAb.delay;
            }
        }
        yield return new WaitForSeconds(delayAbility); // delay time before activating

        foreach (Ability i in ability.abilities)
        {
            if (i.abilityName == "Teleport")
            {
                i.Activate(this, boomerangLocation);
            }
        }
    }
}

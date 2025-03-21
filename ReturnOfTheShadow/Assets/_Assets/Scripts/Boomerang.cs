using System;
using UnityEngine;
using Sirenix.OdinInspector;
public class Boomerang : MonoBehaviour,IReturn
{
    private Rigidbody2D rb;
    [SerializeField] private VariableReference<Vector2> crosshairLocation;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float timeUntilReturn;
    private bool startTimer = false;
    private bool isReturning = false; 
    private float timer = 0f;

    private Vector3 crosshairPosition;
    private Vector3 direction;

    [Title("Object Pool")]
    [SerializeField] private ObjectPool objectPool;
    private GameObject poolObject;
    private void OnEnable()
    {
        poolObject = gameObject;
        startTimer = true;
        isReturning = false;
        rb = GetComponent<Rigidbody2D>();
        timer = 0f; 
        Throw();
    }
    [Button]
    public void DespawnBoomerang()
    {
        poolObject.SetActive(false);
        objectPool.ReturnObject(poolObject);
    }
    private void Throw()
    {
        crosshairPosition = new Vector3(crosshairLocation.value.x, crosshairLocation.value.y, 0);
        direction = (crosshairPosition - transform.position).normalized; 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        rb.linearVelocity = direction * projectileSpeed; 
    }
    
    private void Return()
    {
        if (!isReturning)
        {
            isReturning = true; // Ensure return logic runs only once

            Vector3 playerPosition = FindObjectOfType<PlayerController>().transform.position; 
            direction = (playerPosition - transform.position).normalized; // Move toward the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            rb.linearVelocity = direction * projectileSpeed; // Move toward player
        }
    }

    private void Update()
    {
        if (startTimer)
        {
            CheckReturnTime();
        }
    }

    private void CheckReturnTime()
    {
        if (timer < timeUntilReturn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Return();
        }
    }
}
public interface IReturn
{
    public void DespawnBoomerang();
}

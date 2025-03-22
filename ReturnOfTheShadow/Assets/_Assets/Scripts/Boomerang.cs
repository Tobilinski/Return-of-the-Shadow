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

    private float autoDespawnTime = 3;
    private float despawnTimer;

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
        direction = (crosshairLocation.value - (Vector2)transform.position).normalized;

        transform.right = direction; // Automatically sets rotation

        rb.linearVelocity = direction * projectileSpeed; 
    }
    
    private void Return()
    {
        if (!isReturning)
        {
            isReturning = true; // Makes return logic runs only once

            Vector2 playerPosition = FindObjectOfType<PlayerController>().transform.position; 
            direction = (playerPosition - (Vector2)transform.position).normalized;

            transform.right = direction; // Automatically sets rotation

            rb.linearVelocity = direction * projectileSpeed; // Move toward player
        }
    }

    private void UpdateAutoReturn()
    {
        if (despawnTimer < autoDespawnTime)
        {
            despawnTimer += Time.deltaTime;
        }
        else
        {
            DespawnBoomerang();
            despawnTimer = 0;
        }
    }

    private void Update()
    {
        if(isReturning) UpdateAutoReturn();
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

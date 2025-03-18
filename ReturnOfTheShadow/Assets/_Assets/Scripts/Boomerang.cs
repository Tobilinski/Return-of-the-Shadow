using System;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private VariableReference<Vector2> crosshairLocation;
    [SerializeField] private float projectileSpeed;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 crosshairPosition = new Vector3(crosshairLocation.value.x, crosshairLocation.value.y,0);
        Vector3 direction = crosshairPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        rb.linearVelocity = transform.right * projectileSpeed;
    }
}

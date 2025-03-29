using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    private float length;
    private float startPosition;

    [SerializeField] private GameObject cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * speed;
        float temp = cam.transform.position.x * (1 - speed);
        
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
        
        if(temp > startPosition + length) startPosition += length;
        else if(temp < startPosition - length) startPosition -= length;
    }
}

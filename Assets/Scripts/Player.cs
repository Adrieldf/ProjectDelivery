using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedIncreaseRate;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 currentVelocity = transform.up * initialSpeed;
        float currentSpeed = currentVelocity.magnitude;
        float speedPercentage = currentSpeed / maxSpeed;
        float targetSpeed = Mathf.Min(currentSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
        rb.velocity = currentVelocity.normalized * targetSpeed;
    }
}

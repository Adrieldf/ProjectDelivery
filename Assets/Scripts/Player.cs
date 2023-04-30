using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedIncreaseRate;

    private float rotationAmount = 5f;
    private float minScale = 0.01f;
    private float fallDuration = 100f;
    private bool isFalling = false;
    private bool isDead = false;

    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isDead) {
            rb.velocity = Vector3.zero;
            return;
        }
        Vector3 currentVelocity = transform.up * initialSpeed;
        float currentSpeed = currentVelocity.magnitude;
        float speedPercentage = currentSpeed / maxSpeed;
        float targetSpeed = Mathf.Min(currentSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
        rb.velocity = currentVelocity.normalized * targetSpeed;
    }


    public void FallInHole()
    {
        if (!isFalling)
        {
            isFalling = true;
            isDead = true;
            StartCoroutine(FallAnimation());
        }
    }

    IEnumerator FallAnimation()
    {
        float elapsed = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = new Vector3(minScale, minScale, minScale); 

        while (elapsed < fallDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fallDuration);

            transform.eulerAngles += new Vector3(0,0,rotationAmount);
            transform.Rotate(Vector3.forward * (rotationAmount * Time.deltaTime));

            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            startScale = transform.localScale;

            yield return null;
        }

        isFalling = false;
    }
}

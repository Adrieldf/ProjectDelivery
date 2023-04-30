using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCone : MonoBehaviour
{
    public float launchForce = 10f;
    public float torque = 5f;

    private void Start()
    {
        launchForce = Random.Range(8, 16);
        torque = Random.Range(5, 16);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || !collision.gameObject.CompareTag("Player"))
            return;

        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 launchDirection = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
        rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);

        float torqueDirection = Random.Range(-1f, 1f);
        rb.AddTorque(torqueDirection * torque, ForceMode2D.Impulse);

    }
}

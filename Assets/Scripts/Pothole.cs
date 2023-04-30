using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pothole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || !collision.gameObject.CompareTag("Player"))
            return;


        Player player = collision.gameObject.GetComponent<Player>();
        player.FallInHole();
    }
}

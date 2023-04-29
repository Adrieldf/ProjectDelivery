using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSection : MonoBehaviour
{
    public Transform NextSectionSpawnPosition;
    public Action<Vector3?> spawnNextSectionEvent;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("triggered");
            if (spawnNextSectionEvent != null)
            {
                spawnNextSectionEvent(null);
                spawnNextSectionEvent = null;
                Debug.Log("spawner");
            }
        }
    }
}

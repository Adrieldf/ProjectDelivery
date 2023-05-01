using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{

    public Vector3 target;

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float decreaseRate = 1.0f;

    private bool hasReachedTarget = false;

    void Update()
    {
        if (target != null)
        {
            if (!hasReachedTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                transform.Rotate(0f, 0f, 100 * Time.deltaTime);
                if (transform.position == target)
                    hasReachedTarget = true;
            }
            else
            {
                transform.localScale -= Vector3.one * decreaseRate * Time.deltaTime;
                transform.Rotate(0f, 0f, 200 * Time.deltaTime);

                if (transform.localScale.x <= 0.0f)
                    Destroy(gameObject);
            }
        }
    }
}

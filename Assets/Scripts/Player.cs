using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedIncreaseRate;
    [SerializeField] private Vector3 deliveryDetectionSize;
    [SerializeField] private Vector3 deliveryDetectionOffset;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Delivery delivery;

    private float rotationAmount = 5f;
    private float minScale = 0.01f;
    private float fallDuration = 100f;
    private bool isFalling = false;
    private bool isDead = false;
    private List<int> clientsOrdered;

    void Start()
    {
        clientsOrdered = new List<int>();
    }

    void Update()
    {
        if (isDead)
            return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (transform.position.x > -1)
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (transform.position.x < 1)
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }

        CheckCollisions();
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        Vector3 currentVelocity = transform.up * initialSpeed;
        float currentSpeed = currentVelocity.magnitude;
        float targetSpeed = Mathf.Min(currentSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
        rb.velocity = new Vector2(0f, targetSpeed);


    }


    void CheckCollisions()
    {
        if (!delivery.Idle)
            return;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll((Vector2)(gameObject.transform.position + deliveryDetectionOffset), deliveryDetectionSize, 0f, layerMask);
        // Debug.Log(hitColliders.Length);
        if (hitColliders != null && hitColliders.Length > 0)
        {

            Collider2D firstOrder = hitColliders.Where(r => !clientsOrdered.Any(s => s.Equals(r.gameObject.GetInstanceID())))
                .OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).FirstOrDefault();

            if (firstOrder != null)
            {
                delivery.GenerateOrder(firstOrder.transform.position);
                clientsOrdered.Add(firstOrder.gameObject.GetInstanceID());
                //Debug.Log("Hit : " + collider.name + " Distance: " + distance.ToString());
            }


        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.position + deliveryDetectionOffset, deliveryDetectionSize);
    }

    public void FallInHole()
    {
        if (!isFalling)
        {
            isFalling = true;
            isDead = true;
            StartCoroutine(FallAnimation());
            StartCoroutine(DeathPanel());
        }
    }

    IEnumerator DeathPanel()
    {
        yield return new WaitForSeconds(1f);
        GameController.Instance.OpenDeathPanel();
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

            transform.eulerAngles += new Vector3(0, 0, rotationAmount);
            transform.Rotate(Vector3.forward * (rotationAmount * Time.deltaTime));

            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            startScale = transform.localScale;

            yield return null;
        }

        isFalling = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Delivery : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject movingCircle;
    [SerializeField] private float maxMovingScale;
    [SerializeField] private GameObject targetCircle;

    [SerializeField] private float minTargetTime;
    [SerializeField] private float maxTargetTime;

    [SerializeField] private GameObject[] foods;
    [SerializeField] private GameObject[] orderTypes;
    [SerializeField] private GameObject orderSuccess;
    [SerializeField] private GameObject orderError;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip orderSuccessSfx;
    [SerializeField] private AudioClip orderErrorSfx;

    private float timeLeft;
    private float maxTimeLeft;

    private int currentOrderType;
    private GameObject currentOrderObject;
    private Vector3 orderPositionOffset = new Vector3(0.5f, 1, 0);
    private Vector3 currentTargetPosition;


    public bool Idle = true;

    void Start()
    {
        //ResetCircle();
    }


    void Update()
    {
        if (Idle)
            return;

        if (timeLeft > 0f)
        {
            float scale = (timeLeft / maxTimeLeft) * maxMovingScale;
            movingCircle.transform.localScale = new Vector3(scale, scale, 1);

            timeLeft -= Time.deltaTime;
        }

        bool mouseLeft = Input.GetKeyDown(KeyCode.Mouse0);
        bool mouseRight = Input.GetKeyDown(KeyCode.Mouse1);

        if (mouseLeft || mouseRight)
        {
            if (timeLeft > 0f)
            {
                float scaleMag = movingCircle.transform.localScale.magnitude / 2;

                int score = Mathf.RoundToInt(CalculateBonusMultiplier(scaleMag, 50)) + 100;

                ThrowFood(mouseLeft ? 0 : 1);

                if ((currentOrderType == 0 && mouseLeft) || (currentOrderType == 1 && mouseRight))
                {
                    GameController.Instance.AddScore(score);
                    // Debug.Log(score);
                    InstantiateOrderSuccess();
                }
                else
                {
                    InstantiateOrderError();
                  //  Debug.Log("wrong food");
                }
            }
            else
            {
               // Debug.Log("miss");
                InstantiateOrderError();
            }
        }
        else
        {
            if (timeLeft < 0f)
                InstantiateOrderError();
        }

    }

    private void InstantiateOrderError()
    {
        Instantiate(orderError, currentOrderObject.transform.position, Quaternion.identity);
        Idle = true;
        movingCircle.SetActive(false);
        targetCircle.SetActive(false);
        audioSource.clip = orderErrorSfx;
        audioSource.Play();
    }
    private void InstantiateOrderSuccess()
    {
        Instantiate(orderSuccess, currentOrderObject.transform.position, Quaternion.identity);
        Idle = true;
        movingCircle.SetActive(false);
        targetCircle.SetActive(false);
        audioSource.clip = orderSuccessSfx;
        audioSource.Play();
    }



    public void GenerateOrder(Vector3 position)
    {
        if (!Idle)
            return;

        Idle = false;
        movingCircle.SetActive(true);
        targetCircle.SetActive(true);
        currentOrderType = Random.Range(0, 2);
        currentOrderObject = Instantiate(orderTypes[currentOrderType], position + orderPositionOffset, Quaternion.identity);
        currentTargetPosition = position;

        SetCirclesPosition(position);
        ResetCircle();


    }


    public void ResetCircle()
    {
        maxTimeLeft = timeLeft = Random.Range(minTargetTime, maxTargetTime);
        movingCircle.transform.localScale = new Vector3(5, 5, 0);
    }
    private float CalculateBonusMultiplier(float value, float maxMultiplier)
    {
        float distance = Mathf.Abs(1 - value);
        float multiplier = (1 - Mathf.Pow(distance, 1f)) * maxMultiplier;
        return Mathf.Clamp(multiplier, 0, maxMultiplier);
    }

    private void SetCirclesPosition(Vector3 position)
    {
        position = position + new Vector3(-0.1f, 0, 0);
        movingCircle.transform.position = position;
        targetCircle.transform.position = position;
    }

    private void ThrowFood(int foodType)
    {
        GameObject foodObj = Instantiate(foods[foodType], playerTransform.position, Quaternion.identity);
        foodObj.GetComponent<Throwable>().target = currentTargetPosition;

    }
}

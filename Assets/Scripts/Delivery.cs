using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] private GameObject movingCircle;
    [SerializeField] private float maxMovingScale;
    [SerializeField] private GameObject targetCircle;

    [SerializeField] private float minTargetTime;
    [SerializeField] private float maxTargetTime;

    private float timeLeft;
    private float maxTimeLeft;

    void Start()
    {
        ResetCircle();
    }


    void Update()
    {
        if (timeLeft > 0f)
        {
            float scale = (timeLeft / maxTimeLeft) * maxMovingScale;
            movingCircle.transform.localScale = new Vector3(scale, scale, 1);

            timeLeft -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (timeLeft > 0f)
            {
                float scaleMag = movingCircle.transform.localScale.magnitude / 2;

                int score = Mathf.RoundToInt(CalculateBonusMultiplier(scaleMag, 50)) + 100;
                //add to the current score on screen
                Debug.Log(score);
            }
            else
            {
                Debug.Log("miss");
            }

            ResetCircle();
        }

    }

    public void ResetCircle()
    {
        maxTimeLeft = timeLeft = Random.Range(minTargetTime, maxTargetTime);
        movingCircle.transform.localScale = new Vector3(5, 5, 0);
    }
    public float CalculateBonusMultiplier(float value, float maxMultiplier)
    {
        float distance = Mathf.Abs(1 - value);
        float multiplier = (1 - Mathf.Pow(distance, 1f)) * maxMultiplier;
        return Mathf.Clamp(multiplier, 0, maxMultiplier);
    }

    public void SetCirclesPosition(Vector3 position)
    {
        movingCircle.transform.position = position;
        targetCircle.transform.position = position;
    }
}

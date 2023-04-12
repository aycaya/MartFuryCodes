using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovementHandler : MonoBehaviour
{
    public float deliveryTime = 0.5f;
    [SerializeField] float jumpCoefficient = 5f;
    float currentCounter = 0f;
    float yTarget;
    [HideInInspector] public bool isFlying = false;
    Transform target;
    [SerializeField] Vector3 gameEndJumpForceMin = new Vector3(-1f, 0f, 1f);
    [SerializeField] Vector3 gameEndJumpForceMax = new Vector3(1f, 4f, 3f);
    public Transform Target
    {
        get { return target; }
        set
        {
            target = value;
            isFlying = true;
        }
    }
    bool targetSet = false;
    Vector3 initialPos;

    Rigidbody rb;
    SphereCollider sphereCollider;
    [SerializeField] Vector2 randomVerticalForceMinMax = new Vector2(3f, 6f);
    [SerializeField] Vector2 randomHorizontalForceMinMax = new Vector2(3f, 10f);

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isFlying)
        {
            Fly();
        }
    }

    private void Fly()
    {
        if (!targetSet)
        {
            initialPos = transform.position;
        }
        if (!targetSet)
        {
            yTarget = (target.position.y + (jumpCoefficient));
        }
        targetSet = true;
        currentCounter += Time.deltaTime;
        float normalizedCounter = currentCounter / deliveryTime;
        if (normalizedCounter >= 1)
        {
            targetSet = false;
            currentCounter = 0f;
            //transform.SetParent(target);
            transform.position = target.position;
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            isFlying = false;
            return;
        }
        float xPos = Mathf.Lerp(initialPos.x, target.position.x, normalizedCounter);
        float zPos = Mathf.Lerp(initialPos.z, target.position.z, normalizedCounter);
        float yPos;
        if (normalizedCounter <= 0.5f)
        {
            yPos = Mathf.Lerp(initialPos.y, yTarget, Mathf.Sin(normalizedCounter * Mathf.PI));
        }
        else
        {
            yPos = Mathf.Lerp(yTarget, target.position.y, 1f - Mathf.Sin(normalizedCounter * Mathf.PI));
        }
        transform.position = new Vector3(xPos, yPos, zPos);

    }

    public void SphereJump()
    {
        isFlying = false;
        rb.isKinematic = false;
        sphereCollider.enabled = true;
        float xForce = Random.Range(randomHorizontalForceMinMax.x, randomHorizontalForceMinMax.y);
        float yForce = Random.Range(randomVerticalForceMinMax.x, randomVerticalForceMinMax.y);
        float zForce = Random.Range(randomHorizontalForceMinMax.x, randomHorizontalForceMinMax.y);
        rb.AddForce(new Vector3(xForce, yForce, zForce));
    }

    public void HoleSink()
    {
        isFlying = false;
        rb.isKinematic = false;
        sphereCollider.enabled = true;
    }

    public void WrongColor()
    {
        isFlying = false;
        rb.isKinematic = false;
        sphereCollider.enabled = true;
    }

    public void GameEndJump()
    {
        isFlying = false;
        rb.isKinematic = false;
        sphereCollider.enabled = true;
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        float xForce = Random.Range(gameEndJumpForceMin.x, gameEndJumpForceMax.x);
        float yForce = Random.Range(gameEndJumpForceMin.y, gameEndJumpForceMax.y);
        float zForce = Random.Range(gameEndJumpForceMin.z, gameEndJumpForceMax.z);
        rb.AddForce(new Vector3(xForce, yForce, zForce), ForceMode.VelocityChange);
    }
}

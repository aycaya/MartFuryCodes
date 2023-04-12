using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRot;
    public bool canMove = true;
    Rigidbody rb;
    public bool coolingdown = false;
    [SerializeField] float coolDownTime = 10f;
    public float coolDT = 10f;
    [SerializeField] Transform stopPos;
    Animator animator;
    Vector3 move;
    
    bool arrived = false;
    TruckLoader truckLoader;
    bool isMoving;
    TruckPickUp truckPickUp;
    [SerializeField] Toplayici toplayici;
    
    void Start()
    {
        
        canMove = true;
        startPos = transform.position;
        startRot = transform.rotation;
        animator = GetComponent<Animator>();
        truckLoader = GetComponent<TruckLoader>();
        truckPickUp = GetComponentInChildren<TruckPickUp>();
    }

    void Update()
    {
        if (!coolingdown)
        {

            TruckMoving();


        }
        else
        {
          
            coolDT -= Time.deltaTime;

            if (coolDT <= 0)
            {
                coolingdown = false;
                coolDT = coolDownTime;
            }
        }
        if (arrived)
        {
            canMove = true;
           StartCoroutine(WaitAndStartOver());
          
        }
    }
     void TruckMoving()
    {
        if (!isMoving)
        {
            isMoving = true;
            truckLoader.LoadTruck();

        }
        
        if (arrived)
        {
            return;
        }
      
        move = Vector3.MoveTowards(transform.position, stopPos.position, Time.deltaTime * .5f);
        transform.position = move;
       
    }
    public IEnumerator WaitAndStartOver()
    {

        if (truckPickUp.IsItEmptyCheck() || toplayici.CheckIfFull())
        {

            transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * .5f);
        }
        if (Vector3.Distance(transform.position, startPos) < 0.2f)
        {
            arrived = false;
            coolingdown = true;
            isMoving = false;

        }
        else
        {
            yield return null;
        }
       

    }
     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TruckStop"))
        {
            canMove = false;
            arrived = true;
        }
    }
  
}

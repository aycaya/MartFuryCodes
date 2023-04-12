using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerPickUp : MonoBehaviour
{
    [SerializeField] float waitTimer = 1f;
    float currentTimer = 0;
    bool isCooldownOn = false;
    

    GameObject[] GridsPieces;
    bool[] isGridEmpty;
   
   
    private void Start()
    {
        
        GridsPieces = new GameObject[transform.childCount];
        isGridEmpty = new bool[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            GridsPieces[i] = transform.GetChild(i).gameObject;

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Customer") && !isCooldownOn)
        {
            currentTimer += Time.deltaTime;
          
            if (currentTimer >= waitTimer)
            {

                isCooldownOn = true;
                Invoke("CooldownFinish", 2f);
                currentTimer = 0f;
                PickUpFunc(other.gameObject);
            }
        }
       
        
    }

    private void CooldownFinish()
    {
        isCooldownOn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            currentTimer = 0f;
           
        }
      
    }

    public void PickUpFunc(GameObject picker)
    {
        bool tempTrue = false;
        int index = 0;

        Vector3 Destination = picker.transform.position;
        for (int i = 0; i < GridsPieces.Length; i++)
        {
            if (GridsPieces[i].transform.childCount == 1)
            {
                isGridEmpty[i] = false;
            }
            else if (GridsPieces[i].transform.childCount == 0)
            {
                isGridEmpty[i] = true;
            }
        }

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (!isGridEmpty[i])
            {
                index = i;
                tempTrue = true;

                break;
            }

        }

        if (tempTrue)
        {
            GameObject temp = GridsPieces[index].transform.GetChild(0).gameObject;

            Vector3 initialPoint = temp.transform.position;
            Vector3 direction = (Destination - initialPoint).normalized;


            isGridEmpty[index] = true;

            picker.GetComponent<Toplayici>().OnPickUp(temp.GetComponent<Collactables>());


        }
    }

}


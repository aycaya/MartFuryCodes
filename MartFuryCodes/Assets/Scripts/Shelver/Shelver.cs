using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shelver : MonoBehaviour
{

    [SerializeField] float waitTimer = 1f;
    float currentTimer = 0;
    bool isCooldownOn = false;
    FoodSpawner foodSpawner;

    GameObject[] GridsPieces;
    bool[] isGridEmpty;
    bool coolingdown = false;

    [SerializeField] float coolDownTime = 5f;
    float spawnCDT = 0f;
    bool isItFull;
    GameObject gameObjToUseParent;
    GameObject gameObjToUse;
    [SerializeField] GameObject parentStackObject;

  
    void Start()
    {
        GridsPieces = new GameObject[parentStackObject.transform.childCount];
        isGridEmpty = new bool[parentStackObject.transform.childCount];

        for (int i = 0; i < parentStackObject.transform.childCount; i++)
        {
            GridsPieces[i] = parentStackObject.transform.GetChild(i).gameObject;

        }
    }

   
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("PutIn") && !isCooldownOn)
        {
            currentTimer += Time.deltaTime;
           
            if (currentTimer >= waitTimer)
            {

                isCooldownOn = true;
                Invoke("CooldownFinish", 2f);
                currentTimer = 0f;
                DropOffFunc(other.gameObject);


            }
        }
    }

    private void CooldownFinish()
    {
        isCooldownOn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PutIn"))
        {
            currentTimer = 0f;
            
        }

    }
    void DropOffFunc(GameObject picker)
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

        for (int i = parentStackObject.transform.childCount - 1; i >= 0; i--)
        {
            if (!isGridEmpty[i])
            {
                index = i;
                GameObject temp = GridsPieces[index].transform.GetChild(0).gameObject;
                Collactables collactables = temp.GetComponent<Collactables>();
                Vector3 initialPoint = temp.transform.position;
                Vector3 direction = (Destination - initialPoint).normalized;
                ShelfPicker shelfPicker = picker.GetComponent<ShelfPicker>();


                if (collactables.selectedItem == shelfPicker.selectedItem)
                {
                    shelfPicker.OnPickUp(collactables);
                    isGridEmpty[index] = true;
                    ReposCollactables();
                    break;
                }
                else
                {
                    continue;
                }

            }

        }

    }
    void ReposCollactables()
    {
        int j = 0;
        for (int i = 0; i < parentStackObject.transform.childCount; i++)
        {
            if (GridsPieces[i].transform.childCount == 1)
            {
                GridsPieces[i].transform.GetChild(0).SetParent(GridsPieces[j].transform);
                GridsPieces[j].transform.GetChild(0).transform.localPosition = Vector3.zero;

                j++;
            }
        }
    } 
}
    



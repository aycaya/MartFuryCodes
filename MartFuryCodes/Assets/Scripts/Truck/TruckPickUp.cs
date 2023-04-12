using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TruckPickUp : MonoBehaviour
{
    [SerializeField] Image loadingBar;
    [SerializeField] float waitTimer = 1f;
    float currentTimer = 0;
    [SerializeField] GameObject grayPart;
    bool isCooldownOn = false;
    FoodSpawner foodSpawner;
    [SerializeField] GameObject storage;

    GameObject[] GridsPieces;
    bool[] isGridEmpty;
    bool coolingdown = false;

    [SerializeField] float coolDownTime = 5f;
    float spawnCDT = 0f;
    bool isItFull;
    GameObject gameObjToUseParent;
    GameObject gameObjToUse;
    private void Awake()
    {
        loadingBar.fillAmount = 0f;
        grayPart.SetActive(false);


    }
    private void Start()
    {
        /
        GridsPieces = new GameObject[transform.childCount];
        isGridEmpty = new bool[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            GridsPieces[i] = transform.GetChild(i).gameObject;

        }


    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("TruckStop") && !isCooldownOn)
        {
            currentTimer += Time.deltaTime;
            grayPart.SetActive(true);
            loadingBar.fillAmount = currentTimer / waitTimer;
            if (currentTimer >= waitTimer)
            {
              
                isCooldownOn = true;
                
                Invoke("CooldownFinish", 2f);
                currentTimer = 0f;
              
                loadingBar.fillAmount = 0f;
                grayPart.SetActive(false);

                PickUpFunc(storage.gameObject);
            }
        }

    }

    private void CooldownFinish()
    {
        isCooldownOn = false;
        currentTimer = 0f;
        loadingBar.fillAmount = 0f;
        grayPart.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TruckStop"))
        {
            currentTimer = 0f;
            loadingBar.fillAmount = 0f;
            grayPart.SetActive(false);
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
    public bool IsItEmptyCheck()
    {

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
        isItFull = true;
        for (int i = 0; i < GridsPieces.Length; i++)
        {
            if (!isGridEmpty[i])
            {
                isItFull = false;
                break;
            }

        }
        if (isItFull)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}

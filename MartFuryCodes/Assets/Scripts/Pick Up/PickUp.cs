using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{
    [SerializeField] Image loadingBar;
    [SerializeField] float waitTimer = 1f;
    float currentTimer = 0;
    [SerializeField] GameObject grayPart;
    bool isCooldownOn = false;
    FoodSpawner foodSpawner;

    GameObject[] GridsPieces;
    bool[] isGridEmpty;
    private void Awake()
    {
        loadingBar.fillAmount = 0f;
        grayPart.SetActive(false);
    }
    private void Start()
    {
        foodSpawner = GetComponent<FoodSpawner>();
        GridsPieces = new GameObject[transform.childCount];
        isGridEmpty = new bool[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            GridsPieces[i] = transform.GetChild(i).gameObject;

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isCooldownOn)
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
                PickUpFunc(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Shelver") && !isCooldownOn)
        {
            if (other.GetComponent<ShelverAI>().isItReadyToCollect)
            {
                currentTimer += Time.deltaTime;
                grayPart.SetActive(true);
                loadingBar.fillAmount = currentTimer / waitTimer;

                if (currentTimer >= waitTimer)
                {

                    isCooldownOn = true;
                    currentTimer = 0f;
                    Invoke("CooldownFinish", 2f);
                    loadingBar.fillAmount = 0f;
                    grayPart.SetActive(false);
                    PickUpFunc(other.gameObject);

                }



            }
        }
    }

    private void CooldownFinish()
    {
        isCooldownOn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentTimer = 0f;
            loadingBar.fillAmount = 0f;
            grayPart.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Shelver"))
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

}

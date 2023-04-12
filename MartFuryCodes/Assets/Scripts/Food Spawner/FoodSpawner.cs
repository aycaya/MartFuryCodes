using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    GameObject[] GridsPieces;
    bool[] isGridEmpty;
    bool coolingdown = false;
    [SerializeField] GameObject objectToBeSpawned;
    [SerializeField] float coolDownTime =5f;
     float spawnCDT =0f;
    bool isItFull;
    // Start is called before the first frame update
    void Start()
    {
        GridsPieces = new GameObject[transform.childCount];
        isGridEmpty = new bool[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            GridsPieces[i] = transform.GetChild(i).gameObject;
           
        }

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
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!coolingdown)
        {
            SpawnObjects();
            coolingdown = true;
        }
        else { 

            spawnCDT += Time.deltaTime;
            if (spawnCDT >= coolDownTime)
            {
                spawnCDT = 0f;
                coolingdown = false;

            }
        }
    }
  
    void SpawnObjects()
    {
        if (!IsItFullCheck())
        {
            

            int index = 0;
            while (!isGridEmpty[index])
            {
                index++;

            }
            var objectSpawn = Instantiate(objectToBeSpawned);
            objectSpawn.transform.SetParent(GridsPieces[index].transform);
            isGridEmpty[index] = false;

            objectSpawn.transform.localEulerAngles = Vector3.zero;
            objectSpawn.transform.localPosition =new  Vector3(0,0,0);
        }
        
    }
    bool IsItFullCheck()
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
            if (isGridEmpty[i])
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
    public void  PickUpFunc(GameObject picker)
    {
        bool tempTrue = false;
        int index=0;

        Vector3 Destination = picker.transform.position;

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
            float timer = 0f;

            isGridEmpty[index] = true;
           
            picker.GetComponent<Toplayici>().OnPickUp(temp.GetComponent<Collactables>());
              
        }
    }
    
}

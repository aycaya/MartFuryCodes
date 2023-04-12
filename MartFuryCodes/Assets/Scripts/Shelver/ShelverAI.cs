using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShelverAI : MonoBehaviour
{
    enum ShelverStates { GOTOPRODUCT, COLLECTPRODUCT, GOTOSHELF, PLACEPRODUCT }
    NavMeshAgent agent;
    ShelverStates currentState;
    Transform[] shelfAreas;
    Transform[] productAreas;
    GameObject shelfParent;
    GameObject storageParent;
    List<string> availableProducts = new List<string>();
    List<string> targetProducts = new List<string>();
    int productsToBeCollected = 0;
    [SerializeField] int productState = 0;
    [SerializeField] int productCount = 5;
    [SerializeField] GameObject parentStackObject;
    int shelverSpaceLimit;
    GameObject tomatoParent;
    GameObject pumpkinParent;
    GameObject[] GridsPiecesTomato;
    GameObject[] GridsPiecesPumpkin;
    public bool isItReadyToCollect = false;
    GameObject trashCan;
    GameObject gardenParent;
    float speed;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = PlayerPrefs.GetFloat("ShelverSpeed", 3.5f);
        shelverSpaceLimit = PlayerPrefs.GetInt("ShelverCapacity", 1);
        shelfParent = GameObject.Find("Shelves");
        storageParent = GameObject.Find("Storage");
        gardenParent = GameObject.Find("GARDEN");
        trashCan = GameObject.Find("Trash");
    }
    public void UpdateSpeed()
    {
        speed = PlayerPrefs.GetFloat("ShelverSpeed", 3.5f);
        speed += 0.5f;
        PlayerPrefs.SetFloat("ShelverSpeed", speed);
        agent.speed = PlayerPrefs.GetFloat("ShelverSpeed", 3.5f);
    }
    public void UpdateCapacity()
    {
        var capacity = PlayerPrefs.GetInt("ShelverCapacity", 1);
        capacity++;
        PlayerPrefs.SetInt("ShelverCapacity", capacity);
        shelverSpaceLimit = PlayerPrefs.GetInt("ShelverCapacity", 1);
    }
    // Start is called before the first frame update
    void Start()//
    {
        tomatoParent = gardenParent.transform.GetChild(0).gameObject;
        pumpkinParent = gardenParent.transform.GetChild(1).gameObject;
        GridsPiecesTomato = new GameObject[tomatoParent.transform.childCount];
        GridsPiecesPumpkin = new GameObject[pumpkinParent.transform.childCount];
        shelfAreas = new Transform[shelfParent.transform.childCount];
        productAreas = new Transform[storageParent.transform.childCount];
        
        for (int i = 0; i < tomatoParent.transform.childCount; i++)
        {
            GridsPiecesTomato[i] = tomatoParent.transform.GetChild(i).gameObject;

        }

        for (int i = 0; i < pumpkinParent.transform.childCount; i++)
        {
            GridsPiecesPumpkin[i] = pumpkinParent.transform.GetChild(i).gameObject;

        }
        for (int i = 0; i < shelfParent.transform.childCount; i++)
        {
            shelfAreas[i] = shelfParent.transform.GetChild(i);
        }

        for (int i = 0; i < storageParent.transform.childCount; i++)
        {
            productAreas[i] = storageParent.transform.GetChild(i);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        HandleShelverStates();
        if (agent.remainingDistance >= 0.2f)
        {
            isItReadyToCollect = false;
        }

    }
  
    private void HandleShelverStates()
    {
        switch (currentState)
        {
            case ShelverStates.GOTOPRODUCT:
                GoToProduct();

                break;
            case ShelverStates.COLLECTPRODUCT:
                CollectTheProduct();
                break;
            case ShelverStates.GOTOSHELF:
                GoToProductShelve();
                break;
            case ShelverStates.PLACEPRODUCT:
                PlaceTheProduct();
                break;
        }
    }

    private void GoToProduct()
    {
        if (productState >= productCount)
        {
            productState = 0;
        }
        //Garden
        if (productState == 0)
        {
            for (int i = 0; i < pumpkinParent.transform.childCount; i++)
            {
                Collactables collactables = GridsPiecesPumpkin[i].GetComponentInChildren<Collactables>();
                if (collactables != null)
                {

                    agent.SetDestination(GridsPiecesPumpkin[i].transform.position);
                    break;
                }
            }



        }
        else if (productState == 1)
        {
            for (int i = 0; i < tomatoParent.transform.childCount; i++)
            {
                Collactables collactables = GridsPiecesTomato[i].GetComponentInChildren<Collactables>();
                if (collactables != null)
                {

                    agent.SetDestination(GridsPiecesTomato[i].transform.position);
                    break;
                }
            }

        }
        //Storage
        else if (productState == 2)
        {
            agent.SetDestination(productAreas[0].transform.position);

        }
        else if (productState == 3)
        {
            agent.SetDestination(productAreas[1].transform.position);

        }
        else if (productState == 4)
        {
            agent.SetDestination(productAreas[2].transform.position);

        }

        if (agent.remainingDistance <= 0.2f && agent.hasPath)
        {
            isItReadyToCollect = true;
            agent.ResetPath();
            currentState = ShelverStates.COLLECTPRODUCT;
            return;
        }
    }


    void CollectTheProduct()
    {

        if (parentStackObject.transform.GetChild(shelverSpaceLimit - 1).transform.childCount > 0)
        {

            currentState = ShelverStates.GOTOSHELF;
            return;
        }
    }
    private void GoToProductShelve()
    {
        if (productState == 0)
        {
            agent.SetDestination(shelfAreas[0].position);

        }
        else if (productState == 1)
        {
            agent.SetDestination(shelfAreas[1].position);

        }
        else if (productState == 2)
        {
            agent.SetDestination(shelfAreas[2].position);

        }
        else if (productState == 3)
        {
            agent.SetDestination(shelfAreas[3].position);

        }
        else if (productState == 4)
        {
            agent.SetDestination(shelfAreas[4].position);

        }
        if (agent.remainingDistance <= 0.2f && agent.hasPath)
        {
            agent.ResetPath();

            currentState = ShelverStates.PLACEPRODUCT;
            return;
        }
    }

    void PlaceTheProduct()
    {
        if (shelfAreas[productState].GetComponentInChildren<ShelfPicker>().CheckIfEmptySpaceAvailable() == -1)
        {
            if (parentStackObject.transform.GetChild(0).transform.childCount == 0)
            {

                GoToNextProduct();
            }
            else
            {
                GoToTrashCan();
            }
        }
        else if (parentStackObject.transform.GetChild(0).transform.childCount == 0)
        {

            GoToNextProduct();
        }

    }
    private void GoToNextProduct()
    {
        if (productState >= productCount)
        {
            productState = 0;
        }
        else
        {
            productState++;


        }
        currentState = ShelverStates.GOTOPRODUCT;
        return;
    }
    void GoToTrashCan()
    {

        agent.SetDestination(trashCan.transform.position);

        if (parentStackObject.transform.GetChild(0).transform.childCount == 0)
        {

            GoToNextProduct();
        }
    }
}

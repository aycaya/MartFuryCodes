using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toplayici : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] Transform parentObjectList;
    [SerializeField] float gameEndBallThrowInterval = 0.1f;
    List<Transform> objectList = new List<Transform>();
    [SerializeField] bool isPlayer;
    int playerCapacity;

    private void Awake()
    {
        playerCapacity = PlayerPrefs.GetInt("PlayerCapacity", 3);
        SetOriginalSpherePositions();

    }

    private void SetOriginalSpherePositions()
    {

        if (isPlayer)
        {
            for (int i = 0; i < playerCapacity; i++)
            {
                objectList.Add(parentObjectList.GetChild(i));
            }
        }
        else
        {
            for (int i = 0; i < parentObjectList.childCount; i++)
            {
                objectList.Add(parentObjectList.GetChild(i));
            }
        }

    }
    public void AddPlayerCapacity()
    {
        var currentCpacity = playerCapacity;
        var capacity = PlayerPrefs.GetInt("PlayerCapacity", 3);
        capacity++;
        PlayerPrefs.SetInt("PlayerCapacity", capacity);
        playerCapacity = PlayerPrefs.GetInt("PlayerCapacity", 3);

        for (int i = currentCpacity; i < playerCapacity; i++)
        {
            objectList.Add(parentObjectList.GetChild(i));
        }
    }
    public void OnPickUp(Collactables collectable)
    {


        int availableChildIndex = CheckIfEmptySpaceAvailable();
        if (availableChildIndex == -1)
        {
            return;
        }
        collectable.ToplandiMi = true;

        collectable.SetFlightTarget(objectList[availableChildIndex]);
        collectable.gameObject.transform.SetParent(objectList[availableChildIndex]);

    }

    public int CheckIfEmptySpaceAvailable()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].childCount == 0)
            {
                return i;
            }
        }
        return -1;
    }
    public bool CheckIfFull()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].childCount == 0)
            {
                return false;
            }
        }
        return true;
    }

}

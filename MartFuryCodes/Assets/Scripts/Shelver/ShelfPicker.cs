using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfPicker : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] Transform parentObjectList;
    [SerializeField] float gameEndBallThrowInterval = 0.1f;
    List<Transform> objectList = new List<Transform>();

    public enum WhichItem
    {
        Pumpkin,
        Chicken,
        Bread,
        ToiletPaper,
        Jam,
        Tomato
    };
    public WhichItem theItem;
    public string selectedItem;
    private void Awake()
    {
        SetOriginalSpherePositions();

    }
    private void Start()
    {
        switch (theItem)
        {
            case WhichItem.Pumpkin:
                selectedItem = "Pumpkin";
                break;
            case WhichItem.Chicken:
                selectedItem = "Chicken";
                break;
            case WhichItem.Bread:
                selectedItem = "Bread";
                break;
            case WhichItem.ToiletPaper:
                selectedItem = "ToiletPaper";
                break;
            case WhichItem.Jam:
                selectedItem = "Jam";
                break;
            case WhichItem.Tomato:
                selectedItem = "Tomato";
                break;

            default:

                break;

        }
    }
    private void SetOriginalSpherePositions()
    {
        for (int i = 0; i < parentObjectList.childCount; i++)
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
    public int CheckIfEmpty()
    {
        if (objectList[0].childCount == 0)
        {
            return 1;
        }
        else
        {
            return -1;

        }
    }
}

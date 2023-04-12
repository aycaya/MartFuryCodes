using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckLoader : MonoBehaviour
{
    [SerializeField] int itemCount = 0;
    [SerializeField] GameObject item;


    GameObject truckObject;
    GameObject truckStorageChild;

    private void Awake()
    {
        truckStorageChild = transform.GetChild(0).gameObject;
    }
    public void LoadTruck()
    {
        for (int i = 0; i < itemCount; i++)
        {
            if (truckStorageChild.transform.GetChild(i).childCount > 0)
            {
                continue;
            }
            GameObject temp = Instantiate(item);
            temp.transform.SetParent(truckStorageChild.transform.GetChild(i));
            temp.transform.localPosition = Vector3.zero;
        }

    }
}

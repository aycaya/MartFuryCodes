using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockerManager : MonoBehaviour
{
    [SerializeField] GameObject[] unlockers;
    int currentAreaId;
    void Start()
    {
       SortOutAtStart();
    }

   
    
    public void SortOutUnlockera(int param)
    {
        if (param < unlockers.Length)
        {
           

            unlockers[param].SetActive(true);
        }
       else
        {
            return;
        }
       
    }
    void SortOutAtStart()
    {
        currentAreaId = PlayerPrefs.GetInt("UnlockedAreasNo", 0);
        if (currentAreaId < unlockers.Length)
        {
            for (int i = 0; i < currentAreaId; i++)
            {
                var areaPrnt = unlockers[i].GetComponent<UnlockingAreas>().AreaParent;

                areaPrnt.gameObject.transform.Find(areaPrnt.gameObject.name).gameObject.SetActive(true);
              


            }
            for (int i = 0; i < unlockers.Length; i++)
            {
                unlockers[i].SetActive(false);


            }
            unlockers[currentAreaId].SetActive(true);
        }
    }
}

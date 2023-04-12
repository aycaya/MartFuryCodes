using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTrashCan : MonoBehaviour
{
    public bool coolingdown = false;
    public float coolDT = 10f;
    [SerializeField] float coolDownTime = 10f;

    void Update()
    {
        if (transform.childCount > 0 && !coolingdown)
        {
            CleanTheTrashCan();

            coolingdown = true;
         


        }
        else if(transform.childCount > 0)
        {
          
            coolDT -= Time.deltaTime;

            if (coolDT <= 0)
            {
                coolingdown = false;
                coolDT = coolDownTime;
            }
        }

    }
    void CleanTheTrashCan()
    {
        if (transform.childCount > 0)
        {
            for(int i = transform.childCount-1; i >= 0 ; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}

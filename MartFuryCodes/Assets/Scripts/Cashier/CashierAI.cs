using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CashierAI : MonoBehaviour
{
    GameObject[] cashBoxes;
    Transform emptyCashBox;
    NavMeshAgent agent;
    bool foundCashBox;
    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        cashBoxes = GameObject.FindGameObjectsWithTag("CashBox");
        foreach (GameObject cashBox in cashBoxes)
        {
            var aiParam = cashBox.GetComponent<Cashier>();
            if (!aiParam.hasCashier)
            {
                emptyCashBox = cashBox.transform.Find("CashierPlace");
                foundCashBox = true;
                agent.SetDestination(emptyCashBox.position);
                return;
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.remainingDistance <= 0.2f && agent.hasPath)
        {
            agent.ResetPath();
        }
    }
}

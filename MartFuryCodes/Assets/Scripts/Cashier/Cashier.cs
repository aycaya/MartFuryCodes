using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    GameObject[] productArray;
    [SerializeField] Transform target;
    float waitTimer = 1f;
    float currentTimer = 0;

    public float coinUpwardsModifier = 1f;

    Transform Canvas;
    [SerializeField] GameObject coinPrefab;
    string whichItem;
    public float cashierSpeed;
    public bool hasCashier;
    public bool cashJobIsDone = false;

    // Start is called before the first frame update
    private void Awake()
    {
        cashierSpeed = PlayerPrefs.GetFloat("CashierSpeedCoefficient", 1.6f);

    }
    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CashierPickUpProduct(GameObject parentObject, GameObject customer)
    {
        if (!CashierTrigger.useRegister)
        {
            return;
        }
        if (parentObject.transform.GetChild(0).childCount == 0)
        {

            return;

        }

        for (int i = parentObject.transform.childCount - 1; i >= 0; i--)
        {
            currentTimer += Time.deltaTime;
            if (parentObject.transform.GetChild(i).childCount == 0)
            {
                continue;
            }
            else if (parentObject.transform.GetChild(i).childCount > 0 && currentTimer >= waitTimer)
            {
                currentTimer = 0f;

                GameObject temp = parentObject.transform.GetChild(i).gameObject;
                temp.GetComponentInChildren<ItemMovementHandler>().Target = target;
                temp.transform.GetChild(0).parent = transform;

            }
        }
        if (parentObject.transform.GetChild(0).childCount == 0)
        {

            StartCoroutine(ChangeToCoin(customer));

        }

    }



    IEnumerator ChangeToCoin(GameObject customer)
    {
        if (transform.childCount == 4)
        {
            yield break;
        }


        yield return new WaitForSeconds(0.5f);

        while (transform.childCount - 1 > 3)
        {

            yield return new WaitForSeconds(cashierSpeed);
            if (Vector3.Distance(transform.GetChild(transform.childCount - 1).transform.position, target.position) < 0.1f)
            {

                whichItem = transform.GetChild(transform.childCount - 1).gameObject.GetComponent<Collactables>().selectedItem;
                GameObject p = Instantiate(coinPrefab, target.transform.position, Quaternion.identity);
                p.GetComponent<CoinCollectingAnimForUI>().Item = whichItem;
                p.GetComponent<CoinCollectingAnimForUI>().initialPos = target.transform.position;
                // p.transform.parent = transform;
                Destroy(transform.GetChild(transform.childCount - 1).gameObject);


            }

            yield return null;

        }
        cashJobIsDone = true;
        customer.GetComponent<CustomerAI>().isCashJobDone = true;

    }


}

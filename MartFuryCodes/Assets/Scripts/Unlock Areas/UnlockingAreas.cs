using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;
using TMPro;

public class UnlockingAreas : MonoBehaviour
{
    public int PriceOfArea = 100;
    private int tempPrice;
    private int tempCoinNeed;
    public GameObject AreaParent;
    private GameObject Area;
    PlayerController playerController;
    GameObject player;
    string priceOfAreaID;

    public GameObject CoinMesh;
    MoneyManager moneyManager;
    string isItUnlockedID;

    private bool isItOn = false;
    private bool isItSelled;
    private bool isCoroutineStart = false;

    float startT = 0;
    float T = 0;
    float cooldownTime = 1f;
    bool isDone = false;
    float t2;

    float coinUpwardsModifier = 5f;
    float flightTime = 1f;
    UnlockerManager unlockerManager;
    private void Awake()
    {
        unlockerManager = FindObjectOfType<UnlockerManager>();
    }
    void Start()
    {
        priceOfAreaID = gameObject.name;
        PriceOfArea = PlayerPrefs.GetInt(priceOfAreaID, PriceOfArea);
        cooldownTime = 0.5f;
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        moneyManager = GameObject.FindObjectOfType<MoneyManager>();
        isItUnlockedID = AreaParent.name;
        Area = AreaParent.gameObject.transform.Find(AreaParent.gameObject.name).gameObject;

        tempPrice = PriceOfArea;
        tempCoinNeed = 5;

        if (PlayerPrefs.GetInt(isItUnlockedID, -30) >= 1)
            isItSelled = true;
        else
            isItSelled = false;



    }

    void Update()
    {
        T = Time.time - startT;
        if (isItSelled && !isCoroutineStart)
        {
            isCoroutineStart = true;
            Area.SetActive(true);


            isDone = true;
            t2 = 0.01f;

        }

        if (isDone)
        {
            t2 -= Time.deltaTime;
            if (isItSelled && t2 < 0)
            {

                gameObject.SetActive(false);

            }
        }


        if ((playerController.horizontal == 0 || playerController.vertical == 0) && isItOn && !isCoroutineStart && !isItSelled)
        {
            if (PriceOfArea > 0)
            {
                if (moneyManager.HasEnoughCoin(tempCoinNeed) && T > cooldownTime && tempPrice > 0)
                {
                    StartCoroutine(DropingCoin());
                    startT = Time.time;
                    cooldownTime = cooldownTime / 1.1f;
                    tempPrice = tempPrice - 5;
                    tempCoinNeed += 5;
                }
            }
            if (PriceOfArea <= 0)
            {

                PlayerPrefs.SetInt(isItUnlockedID, 1);
                var param = PlayerPrefs.GetInt("UnlockedAreasNo", 0);
                param++;
                PlayerPrefs.SetInt("UnlockedAreasNo", param);
                unlockerManager.SortOutUnlockera(param);

            }
        }
        if (PriceOfArea <= 0)
        {
            isItSelled = true;


        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (!coll.isTrigger && coll.gameObject.CompareTag("Player"))
        {
            isItOn = true;
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (!coll.isTrigger && coll.gameObject.CompareTag("Player"))
        {
            isItOn = false;
        }
    }

    IEnumerator DropingCoin()
    {
        Vector3 Destination = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        GameObject temp = Instantiate(CoinMesh, new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z), CoinMesh.transform.rotation);
        Vector3 initialPoint = temp.transform.position;
        float rawInitialDistance = Vector3.Distance(Destination, initialPoint);
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (Area.activeSelf)
            {
                Destroy(temp);
                yield break;
            }

            if (Vector3.Distance(temp.transform.position, Destination) < 0.1)
            {
                if (moneyManager.HasEnoughCoin(5) && PriceOfArea > 0)
                {

                    moneyManager.DecreaseCoinValue(5);
                    PriceOfArea -= 5;
                    PlayerPrefs.SetInt(priceOfAreaID, PriceOfArea);
                    tempCoinNeed -= 5;
                }
                Destroy(temp);
                yield break;
            }

            float actualDistance = Vector3.Distance(Destination, temp.transform.position);
            float normalizedCurrentDistance = timer / flightTime;
            float desiredHeightOffset;
            if (normalizedCurrentDistance <= 0.5f)
            {
                desiredHeightOffset = Mathf.Lerp(0f, coinUpwardsModifier, (Mathf.Sin((normalizedCurrentDistance * 2f) * Mathf.PI / 2f)));
            }
            else
            {
                float correctedRatio = (normalizedCurrentDistance - 0.5f) * 2f;
                desiredHeightOffset = Mathf.Lerp(coinUpwardsModifier, 0f, correctedRatio * correctedRatio);
            }
            Vector3 desiredDestiantion = Destination + new Vector3(0f, desiredHeightOffset, 0f);
            temp.transform.position = Vector3.Lerp(initialPoint, desiredDestiantion, normalizedCurrentDistance);
            yield return null;
        }

    }


}


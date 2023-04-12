using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    MoneyManager moneyManager;
    [SerializeField] List<GameObject> tasks = new List<GameObject>();
    [SerializeField] List<TextMeshProUGUI> tasksName = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> tasksPrice = new List<TextMeshProUGUI>();
    GameManager gameManager;
    [SerializeField] GameObject shelverPrefab;
    [SerializeField] Transform door;
    GameObject[] shelvers;
    int[] playerPrefsValues= new int[5] ;
    bool[] alreadyPaid = new bool[5];
    int upgradePrice = 10;
    
    // Start is called before the first frame update
    private void Awake()
    {
       
        moneyManager = FindObjectOfType<MoneyManager>();
        gameManager = FindObjectOfType<GameManager>();
       
        

        if (gameManager.WhatLevel == 1)
        {
            tasksName[0].text = " Shelver Count +1";
            tasksPrice[0].text = upgradePrice.ToString();
            tasksName[1].text = " Player Speed2";
            tasksPrice[1].text = upgradePrice.ToString();
            tasksName[2].text = " Tomato Profit x2";
            tasksPrice[2].text = upgradePrice.ToString();
            tasksName[3].text = " All Profit x2";
            tasksPrice[3].text = upgradePrice.ToString();
            tasksName[4].text = " Shelver Capacity +1";
            tasksPrice[4].text = upgradePrice.ToString();

        }
        else if (gameManager.WhatLevel == 2)
        {
            tasksName[0].text = " Player Speed";
            tasksName[1].text = " Player Speed2";
            tasksName[2].text = " Player Speed3";
            tasksName[3].text = " Player Speed4";
            tasksName[4].text = " Player Speed5";
            tasksName[5].text = " Player Speed6";
        }
        else if (gameManager.WhatLevel == 3)
        {
            tasksName[0].text = " Player Speed";
            tasksName[1].text = " Player Speed2";
            tasksName[2].text = " Player Speed3";
            tasksName[3].text = " Player Speed4";
            tasksName[4].text = " Player Speed5";
            tasksName[5].text = " Player Speed6";
            tasksName[6].text = " Player Speed7";
            tasksName[7].text = " Player Speed8";
        }

        for (int i = 0; i < playerPrefsValues.Length; i++)
        {
            playerPrefsValues[i] = PlayerPrefs.GetInt(("upgrades" + i), -1);
        }
        PlayerPrefsUpgrades();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpgradePurchaseButton(int param)
    {
        if (param == 0)
        {
            if (alreadyPaid[0])
            {
                PlayerPrefs.SetInt("upgrades" + param, 1);
                Instantiate(shelverPrefab, door.position, door.rotation);
                tasks[0].SetActive(false);
            } 
            else if(!alreadyPaid[0] && moneyManager.HasEnoughCoin(upgradePrice))
            {
                moneyManager.DecreaseCoinValue(upgradePrice);
                PlayerPrefs.SetInt("upgrades" + param, 1);
                Instantiate(shelverPrefab, door.position, door.rotation);
                tasks[0].SetActive(false);
            }

          
        }
        else if (param == 1)
        {
            if (alreadyPaid[1])
            {
                PlayerPrefs.SetInt("upgrades" + param, 1);
                tasks[1].SetActive(false);
            }
            else if (!alreadyPaid[1] && moneyManager.HasEnoughCoin(upgradePrice))
            {
                moneyManager.DecreaseCoinValue(upgradePrice);
                PlayerPrefs.SetInt("upgrades" + param, 1);
                tasks[1].SetActive(false);

            }
           
        }
        else if (param == 2)//Item Profit x2
        {
            if (alreadyPaid[2])
            {
                PlayerPrefs.SetInt("upgrades" + param, 1);
                moneyManager.ChangeItemProfits("Tomato", 2);
                tasks[2].SetActive(false);
            }
            else if (!alreadyPaid[2] && moneyManager.HasEnoughCoin(upgradePrice))
            {
                moneyManager.DecreaseCoinValue(upgradePrice);
                PlayerPrefs.SetInt("upgrades" + param, 1);
                moneyManager.ChangeItemProfits("Tomato", 2);
                tasks[2].SetActive(false);
            }
           
        }
        else if (param == 3)// All profit x2
        {
            if (alreadyPaid[3])
            {
                PlayerPrefs.SetInt("upgrades" + param, 1);
                moneyManager.moneyCoefficient *= 2;
                tasks[3].SetActive(false);
            }
            else if (!alreadyPaid[3] && moneyManager.HasEnoughCoin(upgradePrice))
            {
                moneyManager.DecreaseCoinValue(upgradePrice);
                PlayerPrefs.SetInt("upgrades" + param, 1);
                moneyManager.moneyCoefficient *= 2;
                tasks[3].SetActive(false);
            }
           
        }
        else if (param == 4)//Shelver Capacity +1
        {
            if (alreadyPaid[4])
            {
                PlayerPrefs.SetInt("upgrades" + param, 1);
                shelvers = GameObject.FindGameObjectsWithTag("Shelver");

                foreach (GameObject shelver in shelvers)
                {
                    var aiParam = shelver.GetComponent<ShelverAI>();
                    aiParam.UpdateCapacity();
                }

                tasks[4].SetActive(false);
            }
            else if (!alreadyPaid[4] && moneyManager.HasEnoughCoin(upgradePrice))
            {
                moneyManager.DecreaseCoinValue(upgradePrice);
                PlayerPrefs.SetInt("upgrades" + param, 1);
                shelvers = GameObject.FindGameObjectsWithTag("Shelver");

                foreach (GameObject shelver in shelvers)
                {
                    var aiParam = shelver.GetComponent<ShelverAI>();
                    aiParam.UpdateCapacity();
                }

                tasks[4].SetActive(false);
            }
           
        }
        else if (param == 5)
        {
            if (alreadyPaid[5])
            {
                PlayerPrefs.SetInt("upgrades" + param, 1);
                tasks[5].SetActive(false);
            }
            else if (!alreadyPaid[5] && moneyManager.HasEnoughCoin(upgradePrice))
            {
                moneyManager.DecreaseCoinValue(upgradePrice);
                PlayerPrefs.SetInt("upgrades" + param, 1);
                tasks[5].SetActive(false);
            }
           
        }
        else if (param == 6)
        {
            if (alreadyPaid[6])
            {
                tasks[6].SetActive(false);
            }
            else if (!alreadyPaid[6] && moneyManager.HasEnoughCoin(upgradePrice))
            {
                moneyManager.DecreaseCoinValue(upgradePrice);
                tasks[6].SetActive(false);
            }
           
        }
        else if (param == 7)
        {
            if (alreadyPaid[7])
            {
                tasks[7].SetActive(false);
            }
            else if (!alreadyPaid[7] && moneyManager.HasEnoughCoin(upgradePrice))
            {
                moneyManager.DecreaseCoinValue(upgradePrice);
                tasks[7].SetActive(false);
            }
            
        }
       
    }
    void PlayerPrefsUpgrades()
    {
        for (int i = 0; i < playerPrefsValues.Length; i++)
        {
            if (playerPrefsValues[i] == 1)
            {
                alreadyPaid[i] = true;
                UpgradePurchaseButton(i);
            }
            
        }
    }
}

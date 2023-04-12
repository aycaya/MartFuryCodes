using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardMenu : MonoBehaviour
{
    public float SpeedCoeff = 1f;
    public float IncomeCoeff = 1;
    public int GemCoeff = 1;
    public int BaslangicYolGemMiktari = 10;

    public int[] CardBuffAmount;
    public string[] CardBuffID;
    public int[] CardLevel;
    public string[] CardLevelID;
    public int[] maxCardLevel;
    public TextMeshProUGUI[] CardLevelText;
    public TextMeshProUGUI[] UpgradeAmountText;
    public string gemPriceID;
    public int gemPrice = 100;
    public TextMeshProUGUI gemPriceText;

    MoneyManager moneyManager;
    PlayerController playerController;
    Toplayici playerToplayici;

    GameObject[] shelvers;
    GameObject[] cashiers;
    private void Awake()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        playerController = FindObjectOfType<PlayerController>();
        playerToplayici = GameObject.FindGameObjectWithTag("Player").GetComponent<Toplayici>();

        for (int i = 0; i < CardBuffAmount.Length; i++)
        {
            CardBuffAmount[i] = PlayerPrefs.GetInt(CardBuffID[i], CardBuffAmount[i]);
            CardLevel[i] = PlayerPrefs.GetInt(CardLevelID[i], CardLevel[i]);
            CardLevelText[i].text = CardLevel[i].ToString();

        }
        gemPrice = PlayerPrefs.GetInt(gemPriceID, gemPrice);
        gemPriceText.text = gemPrice.ToString();

        if (CardLevel[0] >= 1)
            UpgradeAmountText[0].text = "+%" + CardBuffAmount[0].ToString() + " Player Speed + Capacity";
        if (CardLevel[1] >= 1)
            UpgradeAmountText[1].text = "+%" + CardBuffAmount[1].ToString() + " Income";
        if (CardLevel[2] >= 1)
            UpgradeAmountText[2].text = "+" + CardBuffAmount[2].ToString() + " Shelver Speed + Capacity";
        if (CardLevel[3] >= 1)
            UpgradeAmountText[3].text = "+%" + CardBuffAmount[3].ToString() + " Cashier Speed";

        if (CardLevel[0] >= maxCardLevel[0])
        {
            CardLevelText[0].text = "MAX";
        }

        if (CardLevel[1] >= maxCardLevel[1])
        {
            CardLevelText[1].text = "MAX";
        }

        if (CardLevel[2] >= maxCardLevel[2])
        {
            CardLevelText[2].text = "MAX";
        }

        if (CardLevel[3] >= maxCardLevel[3])
        {
            CardLevelText[3].text = "MAX";
        }
        if (CardLevel[3] >= maxCardLevel[3] && CardLevel[2] >= maxCardLevel[2] && CardLevel[1] >= maxCardLevel[1] && CardLevel[0] >= maxCardLevel[0])
        {
            gemPriceText.text = "MAX";
        }

    }

    public void RandomUpgrade()
    {
        switch (Random.Range(0, 4))
        {
            case 0://Player Speed + Capacity
                if (moneyManager.HasEnoughGem(gemPrice))
                {

                    if (CardLevel[0] >= maxCardLevel[0])
                    {
                        CardLevelText[0].text = "MAX";
                        if (CardLevel[3] < maxCardLevel[3] || CardLevel[2] < maxCardLevel[2] || CardLevel[1] < maxCardLevel[1] || CardLevel[0] < maxCardLevel[0])
                            RandomUpgrade();

                        if (CardLevel[3] >= maxCardLevel[3] && CardLevel[2] >= maxCardLevel[2] && CardLevel[1] >= maxCardLevel[1] && CardLevel[0] >= maxCardLevel[0])
                        {
                            gemPriceText.text = "MAX";
                        }
                        break;
                    }
                    CardBuffAmount[0] += 1;
                    PlayerPrefs.SetInt(CardBuffID[0], CardBuffAmount[0]);
                    UpgradeAmountText[0].text = " Player Speed & Capacity";
                    moneyManager.DecreaseGemValue(gemPrice);
                    gemPrice += 100;
                    gemPriceText.text = gemPrice.ToString();
                    PlayerPrefs.SetInt(gemPriceID, gemPrice);
                    CardLevel[0] += 1;
                    PlayerPrefs.SetInt(CardLevelID[0], CardLevel[0]);
                    CardLevelText[0].text = CardLevel[0].ToString();


                    var speedX = PlayerPrefs.GetInt("Speed", 0);
                    speedX++;
                    PlayerPrefs.SetInt("Speed", speedX);
                    playerController.UpdateSpeed();

                    playerToplayici.AddPlayerCapacity();


                    if (CardLevel[0] >= maxCardLevel[0])
                    {
                        CardLevelText[0].text = "MAX";
                    }
                    if (CardLevel[3] >= maxCardLevel[3] && CardLevel[2] >= maxCardLevel[2] && CardLevel[1] >= maxCardLevel[1] && CardLevel[0] >= maxCardLevel[0])
                    {
                        gemPriceText.text = "MAX";
                    }
                }
                break;
            case 1://Income
                if (moneyManager.HasEnoughGem(gemPrice))
                {
                    if (CardLevel[1] >= maxCardLevel[1])
                    {
                        CardLevelText[1].text = "MAX";
                        if (CardLevel[3] < maxCardLevel[3] || CardLevel[2] < maxCardLevel[2] || CardLevel[1] < maxCardLevel[1] || CardLevel[0] < maxCardLevel[0])
                            RandomUpgrade();
                        if (CardLevel[3] >= maxCardLevel[3] && CardLevel[2] >= maxCardLevel[2] && CardLevel[1] >= maxCardLevel[1] && CardLevel[0] >= maxCardLevel[0])
                        {
                            gemPriceText.text = "MAX";
                        }

                        break;
                    }
                    CardBuffAmount[1] += 1;
                    PlayerPrefs.SetInt(CardBuffID[1], CardBuffAmount[1]);
                    UpgradeAmountText[1].text = " Income";
                    moneyManager.DecreaseGemValue(gemPrice);
                    gemPrice += 100;
                    gemPriceText.text = gemPrice.ToString();
                    PlayerPrefs.SetInt(gemPriceID, gemPrice);
                    CardLevel[1] += 1;
                    PlayerPrefs.SetInt(CardLevelID[1], CardLevel[1]);
                    CardLevelText[1].text = CardLevel[1].ToString();

                    moneyManager.moneyCoefficient += (moneyManager.moneyCoefficient * 10) / 100;
                    PlayerPrefs.SetFloat("IncomeCoefficient", moneyManager.moneyCoefficient);
                    if (CardLevel[1] >= maxCardLevel[1])
                    {
                        CardLevelText[1].text = "MAX";
                    }
                    if (CardLevel[3] >= maxCardLevel[3] && CardLevel[2] >= maxCardLevel[2] && CardLevel[1] >= maxCardLevel[1] && CardLevel[0] >= maxCardLevel[0])
                    {
                        gemPriceText.text = "MAX";
                    }
                }
                break;
            case 2://Shelver Speed+ Capacity
                if (moneyManager.HasEnoughGem(gemPrice))
                {
                    if (CardLevel[2] >= maxCardLevel[2])
                    {
                        CardLevelText[2].text = "MAX";
                        if (CardLevel[3] < maxCardLevel[3] || CardLevel[2] < maxCardLevel[2] || CardLevel[1] < maxCardLevel[1] || CardLevel[0] < maxCardLevel[0])
                            RandomUpgrade();
                        if (CardLevel[3] >= maxCardLevel[3] && CardLevel[2] >= maxCardLevel[2] && CardLevel[1] >= maxCardLevel[1] && CardLevel[0] >= maxCardLevel[0])
                        {
                            gemPriceText.text = "MAX";
                        }
                        break;
                    }
                    CardBuffAmount[2] += 1;
                    PlayerPrefs.SetInt(CardBuffID[2], CardBuffAmount[2]);
                    UpgradeAmountText[2].text = " Shelver Speed & Capacity";
                    moneyManager.DecreaseGemValue(gemPrice);
                    gemPrice += 100;
                    gemPriceText.text = gemPrice.ToString();
                    PlayerPrefs.SetInt(gemPriceID, gemPrice);
                    CardLevel[2] += 1;
                    PlayerPrefs.SetInt(CardLevelID[2], CardLevel[2]);
                    CardLevelText[2].text = CardLevel[2].ToString();


                    shelvers = GameObject.FindGameObjectsWithTag("Shelver");

                    foreach (GameObject shelver in shelvers)
                    {
                        var aiParam = shelver.GetComponent<ShelverAI>();
                        aiParam.UpdateCapacity();
                        aiParam.UpdateSpeed();
                    }

                    if (CardLevel[2] >= maxCardLevel[2])
                    {
                        CardLevelText[2].text = "MAX";
                    }
                    if (CardLevel[3] >= maxCardLevel[3] && CardLevel[2] >= maxCardLevel[2] && CardLevel[1] >= maxCardLevel[1] && CardLevel[0] >= maxCardLevel[0])
                    {
                        gemPriceText.text = "MAX";
                    }
                }
                break;
            case 3://Cashier Speed
                if (moneyManager.HasEnoughGem(gemPrice))
                {

                    if (CardLevel[3] >= maxCardLevel[3])
                    {
                        CardLevelText[3].text = "MAX";
                        if (CardLevel[3] < maxCardLevel[3] || CardLevel[2] < maxCardLevel[2] || CardLevel[1] < maxCardLevel[1] || CardLevel[0] < maxCardLevel[0])
                            RandomUpgrade();
                        if (CardLevel[3] >= maxCardLevel[3] && CardLevel[2] >= maxCardLevel[2] && CardLevel[1] >= maxCardLevel[1] && CardLevel[0] >= maxCardLevel[0])
                        {
                            gemPriceText.text = "MAX";
                        }
                        break;
                    }
                    CardBuffAmount[3] += 1;
                    PlayerPrefs.SetInt(CardBuffID[3], CardBuffAmount[3]);
                    UpgradeAmountText[3].text = " Cashier Speed";
                    moneyManager.DecreaseGemValue(gemPrice);
                    gemPrice += 100;
                    gemPriceText.text = gemPrice.ToString();
                    PlayerPrefs.SetInt(gemPriceID, gemPrice);
                    CardLevel[3] += 1;
                    PlayerPrefs.SetInt(CardLevelID[3], CardLevel[3]);
                    CardLevelText[3].text = CardLevel[3].ToString();


                    cashiers = GameObject.FindGameObjectsWithTag("CashBox");

                    foreach (GameObject cashier in cashiers)
                    {
                        var aiParam = cashier.GetComponent<Cashier>();
                        aiParam.cashierSpeed -= 0.1f * (PlayerPrefs.GetInt(CardLevelID[3]));
                        PlayerPrefs.SetFloat("CashierSpeedCoefficient", aiParam.cashierSpeed);
                    }

                    if (CardLevel[3] >= maxCardLevel[3])
                    {
                        CardLevelText[3].text = "MAX";
                    }
                    if (CardLevel[3] >= maxCardLevel[3] && CardLevel[2] >= maxCardLevel[2] && CardLevel[1] >= maxCardLevel[1] && CardLevel[0] >= maxCardLevel[0])
                    {
                        gemPriceText.text = "MAX";
                    }
                }
                break;
            default:
                Debug.Log("Overflow");
                break;
        }
    }
}

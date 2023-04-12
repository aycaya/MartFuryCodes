using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemText;
    public float moneyCoefficient = 10f;
    public int newMoneyValueCoeff = 2;
    public float[] itemCoeff;
    float currentItemProfit;
    // Start is called before the first frame update
    void Start()
    {
        coinText.text = PlayerPrefs.GetInt("Coin", 0).ToString();
        gemText.text = PlayerPrefs.GetInt("Gem", 0).ToString();
        moneyCoefficient = PlayerPrefs.GetFloat("IncomeCoefficient", 10f);

    }


    public bool HasEnoughCoin(int amount)
    {
        var coin = PlayerPrefs.GetInt("Coin", 0);
        if (coin >= amount)
        {

            return true;
        }
        else
        {
            return false;
        }

    }
    public bool HasEnoughGem(int amount)
    {
        var gem = PlayerPrefs.GetInt("Gem", 0);
        if (gem >= amount)
        {


            return true;
        }
        else
        {
            return false;
        }

    }
    public void IncreaseCoinValue(string itemName)
    {
        int itemLevel = PlayerPrefs.GetInt(itemName, 1);
        ItemProfits(itemName);
        var coin = PlayerPrefs.GetInt("Coin", 0);
        coin += Mathf.RoundToInt(itemLevel * moneyCoefficient * currentItemProfit);
        PlayerPrefs.SetInt("Coin", coin);
        coinText.text = PlayerPrefs.GetInt("Coin", 0).ToString();


    }
    public void IncreaseGemValue(string itemName)
    {
        int itemLevel = PlayerPrefs.GetInt(itemName, 1);
        var gem = PlayerPrefs.GetInt("Gem", 0);
        gem += Mathf.RoundToInt(itemLevel * moneyCoefficient);
        PlayerPrefs.SetInt("Gem", gem);
        gemText.text = PlayerPrefs.GetInt("Gem", 0).ToString();


    }
    public void DecreaseCoinValue(int param)
    {
        var coin = PlayerPrefs.GetInt("Coin", 0);
        coin -= param;
        PlayerPrefs.SetInt("Coin", coin);
        coinText.text = PlayerPrefs.GetInt("Coin", 0).ToString();

    }
    public void DecreaseGemValue(int param)
    {
        var gem = PlayerPrefs.GetInt("Gem", 0);
        gem -= param;
        PlayerPrefs.SetInt("Gem", gem);
        gemText.text = PlayerPrefs.GetInt("Gem", 0).ToString();

    }
    void ItemProfits(string itemName)
    {

        switch (itemName)
        {
            case "Pumpkin":
                currentItemProfit = itemCoeff[0];
                break;
            case "Chicken":
                currentItemProfit = itemCoeff[1];
                break;
            case "Bread":
                currentItemProfit = itemCoeff[2];
                break;
            case "ToiletPaper":
                currentItemProfit = itemCoeff[3];
                break;
            case "Jam":
                currentItemProfit = itemCoeff[4];
                break;
            case "Tomato":
                currentItemProfit = itemCoeff[5];
                break;

            default:

                break;

        }
    }

    public void ChangeItemProfits(string itemName, float coeff)
    {

        switch (itemName)
        {
            case "Pumpkin":
                itemCoeff[0] *= coeff;
                break;
            case "Chicken":
                itemCoeff[1] *= coeff;
                break;
            case "Bread":
                itemCoeff[2] *= coeff;
                break;
            case "ToiletPaper":
                itemCoeff[3] *= coeff;
                break;
            case "Jam":
                itemCoeff[4] *= coeff;
                break;
            case "Tomato":
                itemCoeff[5] *= coeff;
                break;

            default:

                break;

        }
    }

}

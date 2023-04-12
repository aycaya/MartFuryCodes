using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductManager : MonoBehaviour
{
    [SerializeField] GameObject LevelPopUp;
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
    int selectedItemLevel;
    TextMeshProUGUI levelText;
    TextMeshProUGUI PriceText;
    MoneyManager moneyManager;
    GameObject starParent;
    Slider levelSlider;
    int moneyCoeff;
    List<GameObject> temp = new List<GameObject>();

    [SerializeField] int itemLevel1;
    [SerializeField] int itemLevel2;
    [SerializeField] int itemLevel3;
    [SerializeField] int itemLevel4;
    [SerializeField] GameObject shelf1;
    [SerializeField] GameObject shelf2;
    [SerializeField] GameObject shelf3;
    [SerializeField] GameObject shelf4;
    [SerializeField] GameObject startShelf;
    Button activeButton;

    //shelfs childs
    GameObject shelf1Child;
    GameObject shelf2Child;
    GameObject shelf3Child;
    GameObject shelf4Child;
    GameObject startShelfChild;


    int inreaseGem = 10;
    [SerializeField] TextMeshProUGUI gemText;

    // Start is called before the first frame update
    void Start()
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
        shelf1Child = shelf1.gameObject.transform.GetChild(0).gameObject;
        shelf2Child = shelf2.gameObject.transform.GetChild(0).gameObject;
        shelf3Child = shelf3.gameObject.transform.GetChild(0).gameObject;
        shelf4Child = shelf4.gameObject.transform.GetChild(0).gameObject;
        startShelfChild = startShelf.transform.GetChild(0).gameObject;
        moneyManager = FindObjectOfType<MoneyManager>();
        levelText = LevelPopUp.transform.Find("Level_Text").GetComponent<TextMeshProUGUI>();
        starParent = LevelPopUp.transform.Find("Stars").gameObject;
        levelSlider = LevelPopUp.transform.Find("Level_Slider").GetComponent<Slider>();
        PriceText = LevelPopUp.transform.Find("PurchaseButton").transform.Find("Price_Text").GetComponent<TextMeshProUGUI>();
        activeButton = LevelPopUp.transform.Find("PurchaseButton").transform.Find("Active_Button").GetComponent<Button>();

        levelText.text = PlayerPrefs.GetInt(selectedItem, 1).ToString();
        selectedItemLevel = PlayerPrefs.GetInt(selectedItem, 1);
        moneyCoeff = moneyManager.newMoneyValueCoeff;
        PriceText.text = (PlayerPrefs.GetInt(selectedItem, 1) * moneyCoeff).ToString();
        ArrangeShelfsAtStart();
        ArrangeStars();
        ArrangeSlider(true);

    }
    void ArrangeShelfsAtStart()
    {
        var level = PlayerPrefs.GetInt(selectedItem, 1);
        if (level >= 1 && level <= itemLevel1 - 1)
        {

            startShelf.SetActive(true);
            shelf1.SetActive(false);
            shelf2.SetActive(false);
            shelf3.SetActive(false);
            shelf4.SetActive(false);


        }
        else if (level >= itemLevel1 && level <= itemLevel2 - 1)
        {

            startShelf.SetActive(false);
            shelf1.SetActive(true);
            shelf2.SetActive(false);
            shelf3.SetActive(false);
            shelf4.SetActive(false);
        }
        else if (level >= itemLevel2 && level <= itemLevel3 - 1)
        {

            startShelf.SetActive(false);
            shelf1.SetActive(false);
            shelf2.SetActive(true);
            shelf3.SetActive(false);
            shelf4.SetActive(false);
        }
        else if (level >= itemLevel3 && level <= itemLevel4 - 1)
        {

            startShelf.SetActive(false);
            shelf1.SetActive(false);
            shelf2.SetActive(false);
            shelf3.SetActive(true);
            shelf4.SetActive(false);
        }
        else if (level >= itemLevel4)
        {

            startShelf.SetActive(false);
            shelf1.SetActive(false);
            shelf2.SetActive(false);
            shelf3.SetActive(false);
            shelf4.SetActive(true);
        }

    }
    void ArrangeShelfs()
    {
        selectedItemLevel = PlayerPrefs.GetInt(selectedItem, 1);

        if (selectedItemLevel == itemLevel1 && !shelf1.activeSelf)
        {
            //SetActive raf1
            for (int i = 0; i < startShelfChild.transform.childCount; i++)
            {
                if (startShelfChild.transform.GetChild(i).childCount > 0)
                {
                    temp.Add(startShelfChild.transform.GetChild(i).transform.GetChild(0).gameObject);
                }
            }
            if (temp.Count > 0)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    temp[i].transform.SetParent(shelf1Child.transform.GetChild(i).transform);



                    temp[i].transform.localPosition = Vector3.zero;

                    temp[i].transform.localEulerAngles = Vector3.zero;

                }
            }
            temp.Clear();

            startShelf.SetActive(false);
            shelf2.SetActive(false);
            shelf3.SetActive(false);
            shelf4.SetActive(false);

            shelf1.SetActive(true);


        }
        else if (selectedItemLevel == itemLevel2 && !shelf2.activeSelf)
        {


            for (int i = 0; i < shelf1Child.transform.childCount; i++)
            {
                if (shelf1Child.transform.GetChild(i).childCount > 0)
                {
                    temp.Add(shelf1Child.transform.GetChild(i).transform.GetChild(0).gameObject);
                }
            }
            if (temp.Count > 0)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    temp[i].transform.SetParent(shelf2Child.transform.GetChild(i).transform);



                    temp[i].transform.localPosition = Vector3.zero;

                    temp[i].transform.localEulerAngles = Vector3.zero;

                }
            }
            temp.Clear();

            startShelf.SetActive(false);

            shelf1.SetActive(false);
            shelf3.SetActive(false);
            shelf4.SetActive(false);

            shelf2.SetActive(true);


        }
        else if (selectedItemLevel == itemLevel3 && !shelf3.activeSelf)
        {
            //SetActive raf3


            for (int i = 0; i < shelf2Child.transform.childCount; i++)
            {
                if (shelf2Child.transform.GetChild(i).childCount > 0)
                {
                    temp.Add(shelf2Child.transform.GetChild(i).transform.GetChild(0).gameObject);
                }
            }
            if (temp.Count > 0)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    temp[i].transform.SetParent(shelf3Child.transform.GetChild(i).transform);



                    temp[i].transform.localPosition = Vector3.zero;

                    temp[i].transform.localEulerAngles = Vector3.zero;

                }
            }
            temp.Clear();
            startShelf.SetActive(false);

            shelf2.SetActive(false);
            shelf1.SetActive(false);
            shelf4.SetActive(false);

            shelf3.SetActive(true);


        }
        else if (selectedItemLevel == itemLevel4 && !shelf4.activeSelf)
        {
            //SetActive raf4
            for (int i = 0; i < shelf3Child.transform.childCount; i++)
            {
                if (shelf3Child.transform.GetChild(i).childCount > 0)
                {
                    temp.Add(shelf3Child.transform.GetChild(i).transform.GetChild(0).gameObject);
                }
            }
            if (temp.Count > 0)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    temp[i].transform.SetParent(shelf4Child.transform.GetChild(i).transform);



                    temp[i].transform.localPosition = Vector3.zero;

                    temp[i].transform.localEulerAngles = Vector3.zero;

                }
            }
            temp.Clear();
            startShelf.SetActive(false);

            shelf2.SetActive(false);
            shelf3.SetActive(false);
            shelf1.SetActive(false);

            shelf4.SetActive(true);



        }
    }
    void UpdateGem(int val)
    {
        var gem = PlayerPrefs.GetInt("Gem", 0);
        gem += val;
        PlayerPrefs.SetInt("Gem", gem);
        gemText.text = PlayerPrefs.GetInt("Gem", 0).ToString();
    }
    void ArrangeSlider(bool already)
    {
        var level = PlayerPrefs.GetInt(selectedItem, 1);
        if (level >= 1 && level <= itemLevel1 - 1)
        {
            levelSlider.value = (float)(level / 10.0f);
        }
        else if (level == itemLevel1)
        {
            if (!already)
            {
                UpdateGem(inreaseGem * level);
            }



            levelSlider.value = 0f;
        }
        else if (level >= itemLevel1 + 1 && level <= itemLevel2 - 1)
        {
            levelSlider.value = (float)((level - 10) / 15.0f);

        }
        else if (level == itemLevel2)
        {
            if (!already)
            {
                UpdateGem(inreaseGem * level);
            }



            levelSlider.value = 0f;
        }
        else if (level >= itemLevel2 + 1 && level <= itemLevel3 - 1)
        {
            levelSlider.value = (float)((level - 25) / 25.0f);

        }
        else if (level == itemLevel3)
        {
            if (!already)
            {
                UpdateGem(inreaseGem * level);
            }



            levelSlider.value = 0f;
        }
        else if (level >= itemLevel3 + 1 && level <= itemLevel4 - 1)
        {
            levelSlider.value = (float)((level - 50) / 50.0f);
        }
        else if (level == itemLevel4)
        {
            if (!already)
            {
                UpdateGem(inreaseGem * level);
            }



            levelSlider.value = 0f;
        }
    }
    void ArrangeStars()
    {
        var level = PlayerPrefs.GetInt(selectedItem, 1);
        if (level >= 1 && level <= itemLevel1 - 1)
        {
            for (int i = 4; i > 0; i--)
            {
                if (starParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    starParent.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            starParent.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (level >= itemLevel1 && level <= itemLevel2 - 1)
        {
            for (int i = 4; i > 1; i--)
            {
                if (starParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    starParent.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            for (int i = 0; i <= 1; i++)
            {
                if (!starParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    starParent.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        else if (level >= itemLevel2 && level <= itemLevel3 - 1)
        {

            for (int i = 4; i > 2; i--)
            {
                if (starParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    starParent.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            for (int i = 0; i <= 2; i++)
            {
                if (!starParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    starParent.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        else if (level >= itemLevel3 && level <= itemLevel4 - 1)
        {
            for (int i = 4; i > 3; i--)
            {
                if (starParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    starParent.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            for (int i = 0; i <= 3; i++)
            {
                if (!starParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    starParent.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        else if (level >= itemLevel4)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (!starParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    starParent.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (selectedItemLevel == 100 && activeButton.IsInteractable())
        {
            activeButton.interactable = false;
            PriceText.text = "MAX";


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelPopUp.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelPopUp.SetActive(false);

        }

    }
    public void UpgradeItem(Button param)
    {
        selectedItemLevel = PlayerPrefs.GetInt(selectedItem, 1);
        if (selectedItemLevel == 100)
        {
            param.interactable = false;
            PriceText.text = "MAX";

            return;
        }
        if (moneyManager.HasEnoughCoin(selectedItemLevel * moneyCoeff) && selectedItemLevel < 100)
        {
            moneyManager.DecreaseCoinValue(selectedItemLevel * moneyCoeff);
            selectedItemLevel++;
            PlayerPrefs.SetInt(selectedItem, selectedItemLevel);
            levelText.text = PlayerPrefs.GetInt(selectedItem, 1).ToString();
            PriceText.text = (PlayerPrefs.GetInt(selectedItem, 1) * moneyCoeff).ToString();
            ArrangeShelfs();
            ArrangeStars();
            ArrangeSlider(false);


        }

        else
        {
            Debug.Log("Not Enough Coin");
        }


    }
}

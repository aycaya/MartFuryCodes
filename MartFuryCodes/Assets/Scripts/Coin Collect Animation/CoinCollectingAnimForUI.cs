using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectingAnimForUI : MonoBehaviour
{
    RectTransform Target;
    RectTransform Parent;
    private RectTransform Self;

    public int UIlerpSpeed = 8;
    RectTransform CanvasRect;
    bool isItOpen = false;
    public Vector3 initialPos;
    string whichItem;
    int moneyValue;
    MoneyManager moneyManager;
    public string Item
    {

        set
        {
            whichItem = value;


        }
    }
    void Start()
    {
        CanvasRect = GameObject.FindWithTag("Canvas").GetComponent<RectTransform>();
        Self = GetComponent<RectTransform>();
        moneyManager = FindObjectOfType<MoneyManager>();
        Target = GameObject.Find("Coin_Target").GetComponent<RectTransform>();
        Parent = GameObject.Find("Coin_Frame").GetComponent<RectTransform>();
        Vector2 UItransform = new Vector2(Camera.main.WorldToScreenPoint(initialPos).x, Camera.main.WorldToScreenPoint(initialPos).y);
        Self.anchorMin = new Vector2(0f, 0f);
        Self.anchorMax = new Vector2(0f, 0f);
        Self.SetParent(CanvasRect);
        Self.anchoredPosition = new Vector3(UItransform.x, UItransform.y, 0);
        transform.parent = Parent.transform;
        Self.anchorMin = new Vector2(0.5f, 0.5f);
        Self.anchorMax = new Vector2(0.5f, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        Self.anchoredPosition = Vector3.Lerp(Self.anchoredPosition, new Vector3(Target.anchoredPosition.x, Target.anchoredPosition.y), Time.deltaTime * UIlerpSpeed);
        if (Vector3.Distance(Self.anchoredPosition, new Vector3(Target.anchoredPosition.x, Target.anchoredPosition.y)) < 0.2f)
        {
            Reached();
        }

    }

    public void Reached()
    {
        moneyManager.IncreaseCoinValue(whichItem);
        Destroy(gameObject);

    }
}

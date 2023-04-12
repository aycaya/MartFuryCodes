using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collactables : MonoBehaviour
{
    [SerializeField]
    public int Index { get; set; }
    public int colorCode = 0;
    private bool toplandiMi = false;
    ItemMovementHandler itemMovement;
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
    private void Awake()
    {
        itemMovement = GetComponent<ItemMovementHandler>();
    }
    private void Start()
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
    }
    public bool ToplandiMi
    {
        get
        {
            return toplandiMi;
        }
        set
        {

            toplandiMi = value;
        }
    }

    public void SphereJump()
    {
        itemMovement.SphereJump();
    }

    public void HoleSink()
    {
        itemMovement.HoleSink();
    }

    public void SetFlightTarget(Transform target)
    {
        itemMovement.Target = target;
    }

    public void CancelFlight()
    {
        itemMovement.isFlying = false;
    }

    public void WrongColor()
    {
        itemMovement.WrongColor();
    }

    public void DoGameEndJump()
    {
        itemMovement.GameEndJump();
    }

}

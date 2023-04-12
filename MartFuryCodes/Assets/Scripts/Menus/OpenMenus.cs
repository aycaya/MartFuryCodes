using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenus : MonoBehaviour
{
    GameObject uiCanvas;
    GameObject cardMenu;
    GameObject upgradeMenu;
    // Start is called before the first frame update
    void Start()
    {
        uiCanvas = GameObject.Find("UI Canvas");
        cardMenu = uiCanvas.transform.Find("Card").gameObject;
        upgradeMenu = uiCanvas.transform.Find("Upgrade").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenCardMenu()
    {
        cardMenu.SetActive(true);
    }
    public void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);

    }
    public void CloseUpgrade()
    {
        upgradeMenu.SetActive(false);

    }
    public void CloseCardMenu()
    {
        cardMenu.SetActive(false);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public GameObject DeskPrefab;
    public GameObject ShopPrefab;
    public GameObject MenuPrefab;

    public bool OpenInventory;
    public bool OpenDesk;
    public bool OpenShop;
    public bool OpenMenu;


    private GameObject Inventory;
    private GameObject Desk;
    private GameObject Shop;
    private GameObject MenuWindow;


    public void OpenInventorys()
    {
        if (OpenMenu)
        {
            OpenMenu = false;
            Destroy(MenuWindow);
        }
        else
        {
            if (OpenDesk)
            {
                Destroy(Desk);
                OpenDesk = false;
            }
            else
            {
                if (OpenShop)
                {
                    Destroy(Shop);
                    OpenShop = false;
                }
                else
                {
                    if (OpenInventory)
                    {
                        Destroy(Inventory);
                        OpenInventory = false;
                    }
                    else
                    {
                        Inventory = Instantiate(inventoryPrefab);
                        Inventory.transform.SetParent(gameObject.transform);
                        Inventory.GetComponent<RectTransform>().offsetMin = new Vector2(20, 40);
                        Inventory.GetComponent<RectTransform>().offsetMax = new Vector2(-20, -70);
                        Inventory.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        OpenInventory = true;
                    }
                }
            }
        }
    }

    public void OpenDesks()
    {
        if (OpenMenu)
        {
            OpenMenu = false;
            Destroy(MenuWindow);
        }
        else
        {
            if (OpenDesk)
            {
                Destroy(Desk);
                OpenDesk = false;
            }
            else
            {
                if (OpenShop)
                {
                    Destroy(Shop);
                    OpenShop = false;
                }
                else
                {
                    if (OpenInventory)
                    {
                        Destroy(Inventory);
                        OpenInventory = false;
                    }
                    else
                    {
                        Desk = Instantiate(DeskPrefab);
                        Desk.transform.SetParent(gameObject.transform);
                        Desk.GetComponent<RectTransform>().offsetMin = new Vector2(60, 60);
                        Desk.GetComponent<RectTransform>().offsetMax = new Vector2(-20, -70);
                        Desk.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        OpenDesk = true;
                    }
                }
            }
        }
    }

    public void OpenShops()
    {
        if (OpenMenu)
        {
            OpenMenu = false;
            Destroy(MenuWindow);
        }
        else
        {
            if (OpenDesk)
            {
                Destroy(Desk);
                OpenDesk = false;
            }
            else
            {
                if (OpenShop)
                {
                    Destroy(Shop);
                    OpenShop = false;
                }
                else
                {
                    if (OpenInventory)
                    {
                        Destroy(Inventory);
                        OpenInventory = false;
                    }
                    else
                    {
                        Shop = Instantiate(ShopPrefab);
                        Shop.transform.SetParent(gameObject.transform);
                        Shop.GetComponent<RectTransform>().offsetMin = new Vector2(60, 60);
                        Shop.GetComponent<RectTransform>().offsetMax = new Vector2(-20, -70);
                        Shop.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        OpenShop = true;
                    }
                }
            }
        }
    }

    public void OpenMenus()
    {
        if (OpenMenu)
        {
            OpenMenu = false;
            Destroy(MenuWindow);
        }
        else
        {
            if (OpenDesk)
            {
                Destroy(Desk);
                OpenDesk = false;
            }
            else
            {
                if (OpenShop)
                {
                    Destroy(Shop);
                    OpenShop = false;
                }
                else
                {
                    if (OpenInventory)
                    {
                        Destroy(Inventory);
                        OpenInventory = false;
                    }
                    else
                    {
                        MenuWindow = Instantiate(MenuPrefab);
                        MenuWindow.transform.SetParent(gameObject.transform);
                        MenuWindow.GetComponent<RectTransform>().offsetMin = new Vector2(60, 60);
                        MenuWindow.GetComponent<RectTransform>().offsetMax = new Vector2(-20, -70);
                        MenuWindow.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        OpenMenu = true;
                    }
                }
            }
        }
    }
}
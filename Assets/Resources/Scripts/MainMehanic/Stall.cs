using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stall : MonoBehaviour
{
    private GameObject player;
    private GameObject Inventory;
    private SpriteRenderer ShopRenderer;
    private bool AllowClick=false;

    void Start()
    {
        ShopRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        Inventory = GameObject.FindWithTag("Inventory");
        ShopRenderer.sprite = Resources.Load<Sprite>("Object/plate");
    }

    void  OnMouseDown()
    {
        if (AllowClick)
        {
            var menu=Inventory.GetComponent<Menu>();
        menu.OpenShops();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        ShopRenderer.sprite = Resources.Load<Sprite>("Object/plateSelected");
        AllowClick = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        AllowClick = false;
        ShopRenderer.sprite = Resources.Load<Sprite>("Object/plate");
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    private GameObject Inventory;
    private SpriteRenderer DeskRenderer;
    private bool AllowClick=false;

    void Start()
    {
        DeskRenderer = GetComponent<SpriteRenderer>();
        Inventory = GameObject.FindWithTag("Inventory");
    }
    void  OnMouseDown()
    {
         //Inventory.GetComponent<Menu>().OpenDesk();
         
         var menu=Inventory.GetComponent<Menu>();
         if (menu.OpenInventory || menu.OpenDesk) return;
         if (AllowClick)
         {
             menu.OpenDesks();
         }
    }
     private void OnTriggerEnter2D(Collider2D other)
     {
         if (!other.CompareTag("Player")) return;
         DeskRenderer.sprite = Resources.Load<Sprite>("Object/plateSelected");
         AllowClick = true;
     }

     private void OnTriggerExit2D(Collider2D other)
     {
         AllowClick = false;
         DeskRenderer.sprite = Resources.Load<Sprite>("Object/plate");
     }
}

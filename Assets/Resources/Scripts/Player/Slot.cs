using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    private Image SlotImage;
    private Image ItemImage;
    private Text CountyText;

    private int id;

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { Click(); });
    }

    public void FillSlot(int id)
    {
        this.id = id;
        Item item = Player.ListItems[id];
        SlotImage = GetComponent<Image>();
        ItemImage = GetComponentsInChildren<Image>()[1];
        CountyText = GetComponentInChildren<Text>();
        if (item.count > 0)
        {
            CountyText.text = "x" + item.count.ToString();
        }
        else
        {
            CountyText.text = "";
        }

        ItemImage.sprite = Resources.Load<Sprite>(item.imgUrl);
    }

    public void Click()
    {
        if (Inventory.selectedSlot == null)
        {
            SlotImage.sprite = Resources.Load<Sprite>("Canvas/SlotSelected");
            Inventory.selectedSlot = this;
        }
        else if (Inventory.selectedSlot == this)
        {
            SlotImage.sprite = Resources.Load<Sprite>("Canvas/Slot");
            Inventory.selectedSlot = null;
        }
        else
        {
            Inventory.selectedSlot.SlotImage.sprite = Resources.Load<Sprite>("Canvas/Slot");
            (Player.ListItems[Inventory.selectedSlot.id], Player.ListItems[id]) = (Player.ListItems[id], Player.ListItems[Inventory.selectedSlot.id]);
            Inventory.selectedSlot.FillSlot(Inventory.selectedSlot.id);
            FillSlot(id);
            Inventory.selectedSlot = null;
        }
    }
}
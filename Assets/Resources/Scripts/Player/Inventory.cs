using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
   private List<Slot> ListSlot=new List<Slot>();// Список предметів у гравця
   public static Slot selectedSlot=null;// Предмет у руці

   private Text lvlText;
   private Text Money;
   private RectTransform BarProgres;

    void Start()
    {
    
    lvlText=GetComponentsInChildren<Text>()[0];
    Money=GetComponentsInChildren<Text>()[1];
    Money.text+=Player.playerInfo.Money+"$";
    BarProgres=GetComponentsInChildren<Image>()[3].GetComponent<RectTransform>();
    //UI відображення
    BarProgres.localScale=new Vector3(Player.playerInfo.LvlProgress,1,1);
    lvlText.text="LVL"+Player.playerInfo.lvl;

    ListSlot = GetComponentsInChildren<Slot>().ToList();
    int i=0;
    foreach(Slot SlotForeach in ListSlot)
    {
        SlotForeach.FillSlot(i);// Створення предмету у інвенторі
        i++;
    }
    }
    
}

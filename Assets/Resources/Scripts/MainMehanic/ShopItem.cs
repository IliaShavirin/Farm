using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
 private Text price;
 private Text MoneyTxt;
 private Image ProductImage;
 private Text LvlBlockText;
 private Image LvlBlock;

   void Start()
   {
       price = GetComponentsInChildren<Text>()[0];
       ProductImage = GetComponentsInChildren<Image>()[1];
       LvlBlockText = GetComponentsInChildren<Text>()[1];
       LvlBlock = GetComponentsInChildren<Image>()[2];
   }
   
   private void Buy(Item item)
   {
    if(Player.playerInfo.Money>=item.price)
        {
            Player.CheckItem(item);
            Player.playerInfo.Money-=item.price;
            MoneyTxt.text=Player.playerInfo.Money+"$";
            DataBase.Save();
        }
   }

   public void UpdateItem(Item item,Text MoneyTxt)
   {
       ProductImage.sprite=Resources.Load<Sprite>(item.imgUrl);
       price.text=item.price+"$";
       if(item.LvL>Player.playerInfo.lvl)
       {
             GetComponent<Button>().onClick.RemoveAllListeners();
             LvlBlock.enabled=true;
             LvlBlockText.text="Unlock at \n  LvL" + item.LvL.ToString();
             LvlBlockText.enabled=true;
       }
      
       else
       {
       this.MoneyTxt=MoneyTxt;
       LvlBlock.enabled=false;
       LvlBlockText.enabled=false;
       GetComponent<Button>().onClick.RemoveAllListeners();
       GetComponent<Button>().onClick.AddListener(delegate {Buy(item);});
       }
   }
   
}

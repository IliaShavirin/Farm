using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Shop : MonoBehaviour
{
    private List<ShopItem> AllShopItem= new List<ShopItem>();
    private List<Item> AllProductItem= new List<Item>();
    private Text MoneyTxt;

    void Start()
    {
       MoneyTxt = GetComponentsInChildren<Text>()[0];
       MoneyTxt.text=Player.playerInfo.Money+"$";
       AllShopItem =gameObject.GetComponentsInChildren<ShopItem>().ToList();
       FillAllShop();
       StartCoroutine(FillShop());// Використання Unity корутини
    }
    /// <summary>
    /// Заповнити магазин предметами з бази предметів
    /// </summary>
    private void FillAllShop()
    {
        foreach (var item in ItemBaseData.listItem.Where(item => item.type==Item.TYPEFOOD))
        {
            AllProductItem.Add(item);
        }
    }
    /// <summary>
    /// Внутрішній метод для циклу заповення
    /// </summary>
    /// <returns></returns>
     private IEnumerator FillShop()
    {
       yield return new WaitForSeconds(0.3f);
       for(int i=0;i<AllShopItem.Count;i++)
       {
           AllShopItem[i].UpdateItem(AllProductItem[i],MoneyTxt);
       }
    }
}

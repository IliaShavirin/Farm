using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class ItemBaseData : MonoBehaviour
{
    public static string ItemDataBasePath;
    public static List<Item> listItem = new List<Item>();

    public static Item GetItem(string name)
    {
        return listItem.FirstOrDefault(item => item.name == name);
    }
    public static void FillListMedical()
    {
        ItemDataBasePath= Application.persistentDataPath + "/itemDataBase.json";
        if (File.Exists(ItemDataBasePath))
        {
            LoadItems();
        }
        else
        {
            listItem.Add(new Item("Plow", "Item/Tools/Plow", Item.TYPEPLOW, 0, 0, 0, 0.0f, 1f));
            listItem.Add(new Item("Сabbage", "Item/Food/Сabbage/Сabbage", Item.TYPEFOOD, 1, 40, 1, 2f));
            listItem.Add(new Item("Tomato", "Item/Food/Tomato/Tomato", Item.TYPEFOOD, 1, 30, 1, 1f));
            listItem.Add(new Item("Salad", "Item/Food/Salat/Salat", Item.TYPEFOOD, 1, 20, 1, 3f));
            listItem.Add(new Item("Pumpkin", "Item/Food/Pumpkin/Pumpkin", Item.TYPEFOOD, 1, 50, 2, 1f));
            listItem.Add(new Item("Moss", "Item/Other/Mox", Item.TYPEOTHER, 1, 0, 1, 0));
            SaveItemList();
        }

       
    }

    private static void SaveItemList()
    {
        File.WriteAllText(ItemDataBasePath, JsonConvert.SerializeObject(listItem));
    }

    private static void LoadItems()
    {
        listItem = JsonConvert.DeserializeObject<List<Item>>(
            File.ReadAllText(String.Format(ItemDataBasePath)));
    }
    
}
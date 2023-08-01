using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static List<Item> ListItems = new List<Item>();  
    public static PlayerInfo playerInfo;

    /// <summary>
    /// Стандартний метод Unity який викликається під час 1 кадру гри.
    /// </summary>
    void Start()
    {

        playerInfo = DataBase.LoadPlayerInfo();//Завантаження збережень
        this.transform.position = new Vector2(playerInfo._positionx, playerInfo._positiony);
        ListItems = DataBase.LoadInventory();
    }

    /// <summary>
    /// Метод який додає гравцю досвід
    /// </summary>
    /// <param name="exp">Скільки досвіда треба додати</param>
    public static void AddExp(float exp)
    {
        //Призначення досавіду гравцю
        playerInfo.LvlProgress += exp;
        if (!(playerInfo.LvlProgress >= 1)) return;
        playerInfo.lvlstep = playerInfo.lvl;
        playerInfo.lvl++;
        playerInfo.LvlProgress = 0;
    }
    /// <summary>
    /// Метод що дозволяє надати інформацію о предметі у руці
    /// </summary>
    /// <returns>Предмет що у руці</returns>
    public static Item GetHandItem()
    {
        return new Item(ListItems[0].name, ListItems[0].imgUrl, ListItems[0].type, ListItems[0].count,
            ListItems[0].price, ListItems[0].LvL, ListItems[0].timeGrow);
    } 
    /// <summary>
    /// Метод що дозволяє надати інформацію о пустом предметі
    /// </summary>
    /// <returns></returns>
    public static Item GetEmptyItem()
    {
        return new Item("", "Canvas/empty", 0, 0, 0, 0, 0);
    }
    /// <summary>
    /// Видалити предмет у гравця 
    /// </summary>
    public static void RemoveItem()
    {
        if (Player.ListItems[0].count == 1)
        {
            Player.ListItems[0] = GetEmptyItem();
        }
        else
        {
            Player.ListItems[0].count -= 1;
        }

        DataBase.Save();//Збереження гри
    }
    /// <summary>
    /// Перевірити наявність предмета у гравця
    /// </summary>
    /// <param name="item">Який предмет шукаємо</param>
    public static void CheckItem(Item item)
    {
        bool exit = false;
        for (int i = 0; i < ListItems.Count; i++)
        {
            if (item.name == ListItems[i].name)
            {
                //Перевірка на кількість предметів
                ListItems[i].count += item.count;
                exit = true;
                break;
            }
        }

        if (!exit)
        {
            AddItemToInventory(item);//Якщо предмета нема додаємо його
        }

        DataBase.Save();
    }

    /// <summary>
    /// Додати предмет гравцю
    /// </summary>
    /// <param name="item">Предмет що додаємо</param>
    public static void AddItemToInventory(Item item)
    {
        bool added = false;
        for (int i = 0; i < ListItems.Count; i++)
        {
            if (ListItems[i].name == "")
            {
                ListItems[i] = item;
                added = true;
                break;
            }
        }

        if (!added)
        {
            ListItems.Add(item);
        }

        DataBase.Save();
    }

    /// <summary>
    /// Перевірка на виконання завдання
    /// </summary>
    /// <param name="req">Список предметів для виконання завдання</param>
    /// <returns>Виконане завдання чи ні</returns>
    public static bool MiisionItemCheck(List<Item> req)
    {
        int itemsComplete = req.Count;


        for (int i = 0; i < req.Count; i++)
        {
            foreach (Item item in ListItems)
            {
                if (item.name == req[i].name)
                {
                    if (item.count >= req[i].count)
                    {
                        itemsComplete--;//Індексатор залишившихся предметів
                    }
                }
            }
        }

        if (itemsComplete == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// Видалити предмети через завдання
    /// </summary>
    /// <param name="name">Завтра предмету</param>
    /// <param name="count">Кількість</param>
    public static void RemovItemOrMission(string name, int count)
    {
        for (int i = 0; i < ListItems.Count; i++)
        {
            if (ListItems[i].name == name)
            {
                if (ListItems[i].count <= count)
                {
                    ListItems[i] = GetEmptyItem();
                }
                else
                {
                    ListItems[i].count -= count;
                }
            }
        }

        DataBase.Save();
    }

    /// <summary>
    /// Вийти з гри
    /// </summary>
    void OnApplicationQuit()
    {
        DataBase.Save();
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

public class DataBase
{
    //Шляхи до збережень
    public static string inventoryPath = Application.persistentDataPath + "/inventory.path";
    public static string playerInfoPath = Application.persistentDataPath + "/playerData.path";
    public static string ObjectInfoPath = Application.persistentDataPath + "/objectInfo.path";
    private static BinaryFormatter BinaryFormatter = new BinaryFormatter();
    public static bool conservation_exists;
    private static GameObject player;
    public static long difference;
    public static bool Clear;
    
    /// <summary>
    /// Повернути шлях для збереження лунки
    /// </summary>
    /// <param name="id">Індекс луник</param>
    /// <returns>Шлях</returns>
    public static string GetCropPath(int id)
    {
        return Application.persistentDataPath + $"/playerCrop{id}.json";// Використання Unity інструментами для відносного шляху
    }
    /// <summary>
    /// Повернути шлях для збереження завдання
    /// </summary>
    /// <param name="id">Індекс завдання</param>
    /// <returns>Шлях</returns>
    public static string GetOrderPath(int id)
    {
        return Application.persistentDataPath + $"/orderPath{id}.json";
    }
    /// <summary>
    /// Збереження гри
    /// </summary>
    public static void Save()
    {
        SavePlayerInfo();
        SaveInventory();
    }
    /// <summary>
    /// Зберегти інвентар
    /// </summary>
    public static void SaveInventory()
    {
        FileStream stream = new FileStream(inventoryPath, FileMode.Create);
        BinaryFormatter.Serialize(stream, Player.ListItems);
        stream.Close();
    }

    /// <summary>
    /// Зберегти інформацію о гравці
    /// </summary>
    public static void SavePlayerInfo()
    {
        player = GameObject.FindWithTag("Player");// Пошук класу гравця на сцені
        FileStream stream = new FileStream(playerInfoPath, FileMode.Create);
        string currentTime = System.DateTime.Now.ToBinary().ToString();
        PlayerInfo playerInfo = new PlayerInfo(Player.playerInfo.Money, Player.playerInfo.lvl, Player.playerInfo.LvlProgress, true,
            player.transform.position.x, player.transform.position.y, currentTime,Player.playerInfo.lvlstep);
        BinaryFormatter.Serialize(stream, playerInfo);//Збереження через серіалізацію
        stream.Close();
        currentTime = null;
    }

    /// <summary>
    /// Завантажити інвентар
    /// </summary>
    /// <returns>Інвентар</returns>
    public static List<Item> LoadInventory()
    {
        ItemBaseData.FillListMedical();
        if (File.Exists(inventoryPath))
        {
            FileStream stream = new FileStream(inventoryPath, FileMode.Open);
            List<Item> ListItems = (List<Item>)BinaryFormatter.Deserialize(stream);
            stream.Close();
            return ListItems;
        }
        else
        {
            //Інвентар за замовчуванням
            var startItems = new List<Item>();
            startItems.Add(ItemBaseData.GetItem("Plow"));
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            startItems.Add(Player.GetEmptyItem());
            return startItems;
        }
    }

    /// <summary>
    /// Завантажити інформацію о гравці
    /// </summary>
    /// <returns>Інформацію о гравці</returns>
    public static PlayerInfo LoadPlayerInfo()
    {
        if (File.Exists(playerInfoPath))
        {
            FileStream stream = new FileStream(playerInfoPath, FileMode.Open);
            PlayerInfo playerInfo = (PlayerInfo)BinaryFormatter.Deserialize(stream);
            stream.Close();
            difference = getOfflineTime(playerInfo.lastSaveTime);
            return playerInfo;
        }
        else return new PlayerInfo(150, 1, 0f, false, -9f, 4f, "",0);//Інформація гравця якщо збереження немає
    }

    /// <summary>
    /// Видалення усіх збережень
    /// </summary>
    public static void ClearDataBase()
    {
        if (File.Exists(ObjectInfoPath))
        {
            File.Delete(ObjectInfoPath);
        }

        if (File.Exists(inventoryPath))
        {
            File.Delete(inventoryPath);
        }

        if (File.Exists(playerInfoPath))
        {
            File.Delete(playerInfoPath);
        }

        for (int i = 0; i < 100; i++)
        {
            if (File.Exists(GetCropPath(i)))
            {
                File.Delete(GetCropPath(i));
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(GetOrderPath(i)))
            {
                File.Delete(GetOrderPath(i));
            }
        }

        Clear = true;
    }

    /// <summary>
    /// Розрахунок часу не у грі
    /// </summary>
    /// <param name="lastSaveTime">Час</param>
    /// <returns></returns>
    private static long getOfflineTime(string lastSaveTime)
    {
        var currentTime = System.DateTime.Now;
        var lastSaveTimeConvert = System.Convert.ToInt64(lastSaveTime);
        System.DateTime oldTime = System.DateTime.FromBinary(lastSaveTimeConvert);
        System.TimeSpan difference = currentTime.Subtract(oldTime);
        return (long)difference.TotalSeconds;
    }
}
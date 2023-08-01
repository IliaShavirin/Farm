using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class MedicalDataBase : MonoBehaviour
{
    public static List<Medical> MedicalList = new List<Medical>();
    public static string MedicalDataBasePath;

    public static void FillListMedical()
    {
        MedicalDataBasePath = Application.persistentDataPath + "/medicalDataBase.json";
        if (File.Exists(MedicalDataBasePath) && MedicalList.Count == 0)
        {
            LoadMedical();
        }
        else
        {
            MedicalList.Add(new Medical(
                new List<Item> { ItemBaseData.GetItem("Moss"), ItemBaseData.GetItem("Pumpkin") }, 2, "Antibiotic", 100,
                0.2f));
            MedicalList.Add(new Medical(
                new List<Item> { ItemBaseData.GetItem("Salad"), ItemBaseData.GetItem("Tomato") }, 1, "Tomato salad", 50,
                0.1f));
            MedicalList.Add(new Medical(
                new List<Item> { ItemBaseData.GetItem("Moss"), ItemBaseData.GetItem("Tomato") }, 1, "Low Antibiotic", 60,
                0.1f));
            MedicalList.Add(new Medical(
                new List<Item>
                    { ItemBaseData.GetItem("Salad"), ItemBaseData.GetItem("Tomato"), ItemBaseData.GetItem("Сabbage") },
                1, "Cabbage and tomato salad", 70,
                0.15f));
            SaveMedical();
        }
    }

    private static void SaveMedical()
    {
        File.WriteAllText(MedicalDataBasePath, JsonConvert.SerializeObject(MedicalList));
    }

    private static void LoadMedical()
    {
        MedicalList = JsonConvert.DeserializeObject<List<Medical>>(
            File.ReadAllText(MedicalDataBasePath));
    }
}

public class Medical
{
    public int price;
    public float exp;
    public string name;
    public int level;
    public readonly List<Item> listItem;
    /// <summary>
    /// Класс медичного завдання
    /// </summary>
    /// <param name="listItem">Необхідні предмети</param>
    /// <param name="level">Обмеження по рівню</param>
    /// <param name="name">Назва завдання</param>
    /// <param name="price">Ціна завдання</param>
    /// <param name="exp">Нагорода за завдання</param>
    public Medical(List<Item> listItem, int level, string name, int price, float exp)
    {
        this.listItem = listItem;
        this.level = level;
        this.name = name;
        this.price = price;
        this.exp = exp;
    }
}
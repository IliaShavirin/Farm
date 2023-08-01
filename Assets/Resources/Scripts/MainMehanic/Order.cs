using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;
using Random = UnityEngine.Random;

public class Order : MonoBehaviour
{
    // Ui інформація о завданні
    List<Image> ProductImage;
    List<Text> ProductText;
    private Medical Medicals;

    private Text reward;
    private Text Goals;
    private int MoneyToRecive;
    private float ExpToRecive;
    public int id;

    void Start()
    {
        reward = GetComponentsInChildren<Text>()[0];
        Goals = GetComponentsInChildren<Text>()[1];
        ProductImage = GetComponentsInChildren<Image>().ToList();
        ProductText = GetComponentsInChildren<Text>().ToList();
        GetComponentInChildren<Button>().onClick.AddListener(delegate { CompleteOrder(); }); //Unity UI для відображення та праці кнопки
        CreatOrder();//Створення завдання
        SaveOrder();//Сбереження завдання
        Goals.text = Medicals.name;
    }
    /// <summary>
    /// Збереження завдання
    /// </summary>
    private void SaveOrder()
    {
        File.WriteAllText(string.Format(DataBase.GetOrderPath(id)), JsonConvert.SerializeObject(Medicals)); //Збереженя у форматі Json
    }
    /// <summary>
    /// Завантаження завдання
    /// </summary>
    /// <returns>Є завдання с таким індексом чи нема</returns>
    private bool LoadOrder()
    {
        if (File.Exists(DataBase.GetOrderPath(id)))//Перевірка на наявність зберження
        {
            Medicals = JsonConvert.DeserializeObject<Medical>(
                File.ReadAllText(String.Format(DataBase.GetOrderPath(id))));//Зчитування збереження
            return Medicals.listItem.Count > 0;
        }

        return false;
    }
    /// <summary>
    /// Створення завдання
    /// </summary>
    private void CreatOrder()
    {
        MedicalDataBase.FillListMedical();//Лист усіх доступних медичних завданнь
        if (!LoadOrder())
        {
            var ListMedicalLevel = MedicalDataBase.MedicalList.Where(medical1 => medical1.level <= Player.playerInfo.lvl).ToList();// Випадкове завдання що відповідає умоває стає завданням створенним
            var productIndex = Random.Range(1, ListMedicalLevel.Count);
            var medical = MedicalDataBase.MedicalList[productIndex];
            Medicals = medical;
        }

        for (var i = 0; i < Medicals.listItem.Count; i++)
        {
            ProductImage[i + 1].sprite = Resources.Load<Sprite>(Medicals.listItem[i].imgUrl);
            ProductText[i + 3].text = "x" + Medicals.listItem[i].count;
        }

        ExpToRecive = Medicals.exp;
        MoneyToRecive = Medicals.price;
        reward.text = $"You will recive {ExpToRecive} exp and {MoneyToRecive}$";
    }
    /// <summary>
    /// Виконане завдання
    /// </summary>
    private void CompleteOrder()
    {
        if (Player.MiisionItemCheck(Medicals.listItem))
        {
            for (int i = 0; i < Medicals.listItem.Count; i++)
            {
                Player.RemovItemOrMission(Medicals.listItem[i].name, Medicals.listItem[i].count);
            }

            Player.AddExp(ExpToRecive);
            Player.playerInfo.Money += MoneyToRecive;
            File.Delete(DataBase.GetOrderPath(id));//Видалення збереження
            DataBase.Save();
            Destroy(gameObject);
        }
    }
    
}
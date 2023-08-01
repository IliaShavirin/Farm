using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item 
{
    //Константи типів предметів
    public static int TYPEFOOD = 1;
    public static int TYPEPLOW = 2;
    public static int TYPEOTHER = 3;

    public string name;
    public string imgUrl;
    public int type;
    public int count;
    public int price;
    public int LvL;
    public float timeGrow;
    public int step;
    public float strength;
    /// <summary>
    /// Предмет
    /// </summary>
    /// <param name="name">Його назва</param>
    /// <param name="imgUrl">Його адресса до зображення</param>
    /// <param name="type">Його тип</param>
    /// <param name="count">Його кількість</param>
    /// <param name="price">Його ціна</param>
    /// <param name="LvL">Його обмеження по рівню</param>
    /// <param name="timeGrow">Його час зростання 1 єтапу</param>
    /// <param name="strength">Його прочність</param>
    /// <param name="step">Його єтап</param>
    public Item(string name,string imgUrl, int type, int count, int price, int LvL, float timeGrow,float strength=1,int step=1)
    {
        this.name = name;
        this.imgUrl = imgUrl;
        this.type = type;
        this.count = count;
        this.price = price;
        this.LvL = LvL;
        this.timeGrow = timeGrow;
        this.strength=strength;
        this.step = step;
    }
}

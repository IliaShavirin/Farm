using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using Newtonsoft.Json;

public class Crop : MonoBehaviour
{
    private const int STEP_EMPTY = 0;
    private const int STEP_GROWSONE = 1;
    private const int STEP_GROWSTWO = 2;
    private const int STEP_THERE = 3;
    private const int STEP_PRODUCT = 5;
    private const int STEP_PRODUCTS = 4;
    private const int STEP_PLOW = 6;

    private SpriteRenderer ItemRenderer;
    private SpriteRenderer CropRenderer;


    public Item cropItem;
    public float timeGrow;
    public int step = 0;
    private GameObject player;
    private GameObject destroy;
    private bool ForReadyGrow;
    private bool Search;
    private bool _distTrigger;
    private bool NameLoad;
    private bool Updates;
    private int id;
    private static int _id;
    private string name;
    private string timeGrowStarted = string.Empty;

    public void Clears()
    {
        if (File.Exists(DataBase.GetCropPath(id)))
        {
            File.Delete(DataBase.GetCropPath(id));
        }
    }

    void Start()
    {
        _id++;
        id = _id;
        CropRenderer = GetComponent<SpriteRenderer>();
        ItemRenderer = GetComponentsInChildren<SpriteRenderer>()[1];

        player = GameObject.FindWithTag("Player");

        timeGrow = cropItem.timeGrow;
        if (File.Exists(DataBase.GetCropPath(id)))
        {
            LoadCropInfo();
            step = cropItem.step;

            TimeOffline();
            switch (step)
            {
                case STEP_GROWSONE:
                    ItemRenderer.sprite = Resources.Load<Sprite>($"Item/Food/{cropItem.name}/{cropItem.name}" + $"{1}");
                    cropItem.count = 2;
                    break;
                case STEP_GROWSTWO:
                    ItemRenderer.sprite =
                        Resources.Load<Sprite>($"Item/Food/{cropItem.name}/{cropItem.name}" + $"{2}");
                    cropItem.count = 2;
                    break;
                case STEP_THERE:
                    ItemRenderer.sprite =
                        Resources.Load<Sprite>($"Item/Food/{cropItem.name}/{cropItem.name}" + $"{3}");
                    cropItem.count = 2;
                    break;
                case STEP_PRODUCT:
                    ItemRenderer.sprite = Resources.Load<Sprite>($"Item/Food/{cropItem.name}/{cropItem.name}" + $"{4}");
                    cropItem.count = 2;
                    break;
                default:
                    break;
            }

            Plants(cropItem);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _distTrigger = true;
        if (!other.CompareTag("Player")) return;
        switch (step)
        {
            case STEP_EMPTY:
            case STEP_PRODUCT:

                CropRenderer.sprite = Resources.Load<Sprite>("Item/Food/cropSelected");
                ForReadyGrow = true;

                break;

            case STEP_PLOW:

                CropRenderer.sprite = Resources.Load<Sprite>("Item/Food/cropBrokenSekected");
                ForReadyGrow = true;
                break;

            default:
                CropRenderer.sprite = Resources.Load<Sprite>("Item/Food/crop");
                ForReadyGrow = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _distTrigger = false;
        switch (step)
        {
            case STEP_EMPTY:
            case STEP_PRODUCT:

                CropRenderer.sprite = Resources.Load<Sprite>("Item/Food/crop");
                ForReadyGrow = false;

                break;

            case STEP_PLOW:
                CropRenderer.sprite = Resources.Load<Sprite>("Item/Food/cropBroken");
                ForReadyGrow = false;

                break;

            default:
                CropRenderer.sprite = Resources.Load<Sprite>("Item/Food/crop");
                ForReadyGrow = false;
                break;
        }

        _distTrigger = false;
    }

    private void TimeOffline()
    {
        if (DataBase.difference > cropItem.timeGrow && step != 0 && step < 4)
        {
            step++;
        }

        if (DataBase.difference > cropItem.timeGrow * 2 && step != 0 && step < 4)
        {
            step++;
        }

        if (DataBase.difference > cropItem.timeGrow * 3 && step != 0 && step < 4)
        {
            step++;
        }

        if (DataBase.difference > cropItem.timeGrow * 4 && step != 0 && step < 4)
        {
            step++;
        }
    }

    private void SaveCrop()
    {
        cropItem.step = step;
        File.WriteAllText(DataBase.GetCropPath(id), JsonConvert.SerializeObject(cropItem));
    }

    private void LoadCropInfo()
    {
        cropItem = JsonConvert.DeserializeObject<Item>(
            File.ReadAllText(String.Format(DataBase.GetCropPath(id))));
        ForReadyGrow = true;
    }
    

    void OnMouseDown()
    {
        if (!_distTrigger) return;
        Item item = Player.GetHandItem();
        Plants(item);
    }

    private void Plants(Item item)
    {
        if (ForReadyGrow)
        {
            switch (step)
            {
                case STEP_EMPTY:

                    if (item.type == Item.TYPEFOOD)
                    {
                        Player.RemoveItem();
                        cropItem = item;
                        cropItem.count = 2;
                        ItemRenderer.sprite =
                            Resources.Load<Sprite>($"Item/Food/{cropItem.name}/{cropItem.name}" + $"{0}");
                        timeGrowStarted = System.DateTime.Now.ToBinary().ToString();
                        StartCoroutine(Grow());
                    }

                    break;

                case STEP_PRODUCT:
                    CropRenderer.sprite = Resources.Load<Sprite>("Item/Food/cropBroken");
                    ItemRenderer.sprite = Resources.Load<Sprite>("");
                    cropItem.count = 2;
                    step = STEP_PLOW;
                    Player.CheckItem(cropItem);

                    break;

                case STEP_PLOW:
                    if (item.type == Item.TYPEPLOW)
                    {
                        CropRenderer.sprite = Resources.Load<Sprite>("");
                        step = STEP_EMPTY;
                    }

                    break;

                default:
                    StartCoroutine(Grow());
                    break;
            }
        }
    }

    private IEnumerator Grow()
    {
        for (int i = 0; i < 5; i++)
        {
            SaveCrop();
            switch (step)
            {
                case STEP_EMPTY:
                    yield return new WaitForSeconds(timeGrow);
                    ItemRenderer.sprite = Resources.Load<Sprite>($"Item/Food/{cropItem.name}/{cropItem.name}" + $"{0}");
                    step = STEP_GROWSONE;
                    timeGrow = cropItem.timeGrow;
                    break;
                case STEP_GROWSONE:
                    yield return new WaitForSeconds(timeGrow);
                    ItemRenderer.sprite =
                        Resources.Load<Sprite>($"Item/Food/{cropItem.name}/{cropItem.name}" + $"{1}");
                    step = STEP_GROWSTWO;
                    timeGrow = cropItem.timeGrow;
                    break;
                case STEP_GROWSTWO:
                    yield return new WaitForSeconds(timeGrow);
                    ItemRenderer.sprite = Resources.Load<Sprite>($"Item/Food/{cropItem.name}/{cropItem.name}" + $"{2}");
                    step = STEP_THERE;
                    timeGrow = cropItem.timeGrow;
                    break;
                case STEP_THERE:
                    yield return new WaitForSeconds(timeGrow);
                    ItemRenderer.sprite =
                        Resources.Load<Sprite>($"Item/Food/{cropItem.name}/{cropItem.name}" + $"{3}");
                    step = STEP_PRODUCTS;
                    timeGrow = cropItem.timeGrow;
                    break;
                case STEP_PRODUCTS:
                    yield return new WaitForSeconds(timeGrow);
                    ItemRenderer.sprite =
                        Resources.Load<Sprite>($"Item/Food/{cropItem.name}/{cropItem.name}" + $"{4}");
                    step = STEP_PRODUCT;
                    break;
            }
        }
    }

    void OnApplicationQuit()
    {
        //SaveCrop();
    }
}
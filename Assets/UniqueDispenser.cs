using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueDispenser : MonoBehaviour
{
    [SerializeField]
    private string uniqueItem;

    private bool _distance;

    private void OnTriggerEnter2D(Collider2D other)
    {  if (!other.CompareTag("Player")) return;
        _distance = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _distance = false;
    }

    private void OnMouseDown()
    {
        if (_distance)
        {
           Player.CheckItem(ItemBaseData.GetItem(uniqueItem));
        }
    }
}

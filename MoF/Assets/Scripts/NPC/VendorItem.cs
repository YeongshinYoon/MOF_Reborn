using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VendorItem
{
    public Item item;

    public int stack;

    public void setItem(Item item)
    {
        this.item = item;
    }
}

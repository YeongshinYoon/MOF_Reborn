using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField]
    private Item item;
    public Item MyItem
    {
        get
        {
            return item;
        }
    }

    [SerializeField]
    private float dropChance;
    public float MyDropChance
    {
        get
        {
            return dropChance;
        }
    }
}

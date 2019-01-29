using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorSell : MonoBehaviour 
{
    private static VendorSell instance;

    public static VendorSell MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<VendorSell>();
            }
            return instance;
        }
    }

    private VendorItem[] items;

    public VendorItem[] MyItems
    {
        get
        {
            return items;
        }
    }

    public void items_bag1()
    {
        int index = 0;
        items = new VendorItem[InventoryScript.MyInstance.MyFullSlotCount];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new VendorItem();
        }

        for (int i = 0; i < InventoryScript.MyInstance.MyTotalSlotCount; i++)
        {
            if (!InventoryScript.MyInstance.MySlots[i].IsEmpty)
            {
                items[index].item = InventoryScript.MyInstance.MySlots[i].MyItem;
                items[index].stack = InventoryScript.MyInstance.MySlots[i].MyCount;
                index++;
                if (index >= InventoryScript.MyInstance.MyFullSlotCount)
                {
                    break;
                }
            }
        }
    }

    public void items_bag2()
    {
        int index = 0;
        items = new VendorItem[InventoryScript.MyInstance.MyFullSlotCount2];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new VendorItem();
        }

        for (int i = 0; i < InventoryScript.MyInstance.MyTotalSlotCount2; i++)
        {
            if (!InventoryScript.MyInstance.MySlots2[i].IsEmpty)
            {
                items[index].item = InventoryScript.MyInstance.MySlots2[i].MyItem;
                index++;
                if (index >= InventoryScript.MyInstance.MyFullSlotCount2)
                {
                    break;
                }
            }
        }
    }

    public void items_bag3()
    {
        int index = 0;
        items = new VendorItem[InventoryScript.MyInstance.MyFullSlotCount3];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new VendorItem();
        }

        for (int i = 0; i < InventoryScript.MyInstance.MyTotalSlotCount3; i++)
        {
            if (!InventoryScript.MyInstance.MySlots3[i].IsEmpty)
            {
                items[index].item = InventoryScript.MyInstance.MySlots3[i].MyItem;
                index++;
                if (index >= InventoryScript.MyInstance.MyFullSlotCount3)
                {
                    break;
                }
            }
        }
    }
}

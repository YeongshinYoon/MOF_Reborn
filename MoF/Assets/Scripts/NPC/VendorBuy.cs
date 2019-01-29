using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorBuy : MonoBehaviour 
{
    private static VendorBuy instance;

    public static VendorBuy MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<VendorBuy>();
            }
            return instance;
        }
    }

    [SerializeField]
    private VendorBuyItem[] vendorBuyItems;

    private VendorItem[] currentItems;

    public VendorItem[] MyCurrentItems
    {
        get
        {
            return currentItems;
        }
    }

    public void SetCurrentItems(string NPCName)
    {
        foreach (VendorBuyItem vbi in vendorBuyItems)
        {

            if (vbi.MyNPCName == NPCName)
            {
                currentItems = vbi.MyItems;
            }
        }
    }
}

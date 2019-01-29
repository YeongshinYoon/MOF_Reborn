using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VendorBuyItem : MonoBehaviour 
{
    [SerializeField]
    private string NPCName;

    public string MyNPCName
    {
        get
        {
            return NPCName;
        }
    }

    [SerializeField]
    private VendorItem[] items;

    public VendorItem[] MyItems
    {
        get
        {
            return items;    
        }
    }
}

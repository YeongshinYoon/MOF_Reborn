using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject, IMovable, IDescribable {
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;

    [SerializeField]
    private int price;
    
    private SlotScript slot;

    [SerializeField]
    private int itemNumber;

    private EquipmentButton equipmentButton;

    public EquipmentButton MyEquipmentButton
    {
        get
        {
            return equipmentButton;
        }

        set
        {
            MySlot = null;
            equipmentButton = value;
        }
    }

    [SerializeField]
    private bool isRemovable;

    public string MyTitle
    {
        get
        {
            return title;
        }
    }

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public Quality MyQuality
    {
        get
        {
            return quality;
        }
    }

    public int MyPrice
    {
        get
        {
            return price;
        }
    }

    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public SlotScript MySlot
    {
        get
        {
            return slot;
        }

        set
        {
            slot = value;
        }
    }

    public int MyItemNumber
    {
        get
        {
            return itemNumber;
        }
    }

    public bool IsRemovable
    {
        get
        {
            return isRemovable;
        }
    }

    public virtual string GetDescription()
    {
        return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[quality], title);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }

    public void MustRemove()
    {
        if (MySlot != null)
        {
            MySlot.MustRemoveItem(this);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler {
    private ObservableStack<Item> items = new ObservableStack<Item>();

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSize;

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    public bool IsFull
    {
        get
        {
            if (IsEmpty || (MyCount < MyItem.MyStackSize))
            {
                return false;
            }
            return true;
        }
    }

    public Item MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return items.Peek();
            }

            return null;
        }
    }

    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get
        {
            return items.Count;
        }
    }

    public Text MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public ObservableStack<Item> MyItems
    {
        get
        {
            return items;
        }
    }

    private void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    public bool AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;

        return true;
    }

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || (newItems.Peek().GetType() == MyItem.GetType()))
        {
            int count = newItems.Count;

            for (int i=0; i<count; i++)
            {
                if (IsFull)
                {
                    return false;
                }
                AddItem(newItems.Pop());
            }
            return true;
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty && item.IsRemovable)
        {
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
        }
    }

    public void MustRemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty)
            {
                if (HandScript.MyInstance.MyMovable != null)
                {
                    if (HandScript.MyInstance.MyMovable is Armor)
                    {
                        if (MyItem is Armor && (MyItem as Armor).MyArmorType == (HandScript.MyInstance.MyMovable as Armor).MyArmorType)
                        {
                            (MyItem as Armor).Equip();
                            HandScript.MyInstance.Drop();
                        }
                    }
                }
                else
                {
                    HandScript.MyInstance.TakeMovable(MyItem as IMovable);
                    InventoryScript.MyInstance.FromSlot = this;
                }
            }
            else if (InventoryScript.MyInstance.FromSlot == null && IsEmpty)
            {
                if (HandScript.MyInstance.MyMovable is Armor)
                {
                    Armor armor = (Armor)HandScript.MyInstance.MyMovable;
                    Equipment.MyInstance.MySelectedButton.DequipArmor();
                    AddItem(armor);
                    HandScript.MyInstance.Drop();
                }
            }
            else if (InventoryScript.MyInstance.FromSlot != null)
            {
                if (PutItemBack() || MergeItems(InventoryScript.MyInstance.FromSlot) || SwapItems(InventoryScript.MyInstance.FromSlot) || AddItems(InventoryScript.MyInstance.FromSlot.items))
                {
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.FromSlot = null;
                }
            }
        }

        if(eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMovable == null)
        {
            UseItem();
        }
    }

    public void Clear()
    {
        int initCount = MyItems.Count;

        if (initCount > 0)
        {
            for (int i = 0; i < initCount; i++)
            {
                InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            }
        }
    }

    public void ClearItem()
    {
        MyItems.Clear();
    }

    public void UseItem()
    {
        if (MyItem is IUsable)
        {
            (MyItem as IUsable).use();
        }
        else if (MyItem is Armor)
        {
            (MyItem as Armor).Equip();
        }
    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == MyItem.name && items.Count < MyItem.MyStackSize)
        {
            items.Push(item);
            item.MySlot = this;
            return true;
        }
        return false;
    }

    private bool PutItemBack()
    {
        if (InventoryScript.MyInstance.FromSlot == this)
        {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }

    private bool SwapItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if((from.MyItem.GetType() != MyItem.GetType()) || (from.MyCount+MyCount > MyItem.MyStackSize))
        {
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.items);

            from.items.Clear();
            from.AddItems(items);

            items.Clear();
            AddItems(tmpFrom);

            return true;
        }
        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if ((from.MyItem.GetType() == MyItem.GetType()) && !IsFull)
        {
            int free = MyItem.MyStackSize - MyCount;

            for (int i=0; i<free; i++)
            {
                if (from.items.Count > 0)
                {
                    AddItem(from.items.Pop());
                }
            }
            return true;
        }
        return false;
    }

    private void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            UIManager.MyInstance.ShowTooltip(transform.position, MyItem);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}

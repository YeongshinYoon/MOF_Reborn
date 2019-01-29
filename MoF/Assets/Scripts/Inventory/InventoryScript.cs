using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ItemCountChanged(Item item);

public class InventoryScript : MonoBehaviour {
    public event ItemCountChanged itemCountChangedEvent;

    private static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }

            return instance;
        }
    }

    private int currentBagNumber;

    [SerializeField]
    private Sprite activated;

    [SerializeField]
    private Sprite nonactivated;

    [SerializeField]
    private GameObject bag1;

    [SerializeField]
    private GameObject bag2;

    [SerializeField]
    private GameObject bag3;

    [SerializeField]
    private Button bag1btn;

    [SerializeField]
    private Button bag2btn;

    [SerializeField]
    private Button bag3btn;

    [SerializeField]
    private List<SlotScript> slots = new List<SlotScript>();

    [SerializeField]
    private List<SlotScript> slots2 = new List<SlotScript>();

    [SerializeField]
    private List<SlotScript> slots3 = new List<SlotScript>();

    [SerializeField]
    private List<SlotScript> buyBasket = new List<SlotScript>();

    [SerializeField]
    private List<SlotScript> sellBasket = new List<SlotScript>();

    public List<SlotScript> MySlots 
    {
        get 
        {
            return slots;
        }
    }

    public List<SlotScript> MySlots2
    {
        get
        {
            return slots2;
        }
    }

    public List<SlotScript> MySlots3
    {
        get
        {
            return slots3;
        }
    }

    [SerializeField]
    private CanvasGroup canvasGroup;

    private SlotScript fromSlot;

    [SerializeField]
    private Text Ribi_t;

    private int Ribi;

    public int MyRibi
    {
        get
        {
            return Ribi;
        }
    }

    [SerializeField]
    private Item[] items;

    public Item[] MyItems
    {
        get
        {
            return items;
        }
    }

    public int MyEmptyBuyBasketCount
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in buyBasket)
            {
                if (slot.IsEmpty)
                {
                    count++;
                }
            }

            return count;
        }
    }

    public int MyTotalBuyBasketCount
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in buyBasket)
            {
                count++;
            }

            return count;
        }
    }

    public int MyFullBuyBasketCount
    {
        get
        {
            return MyTotalBuyBasketCount - MyEmptyBuyBasketCount;
        }
    }

    public int MyEmptySellBasketCount
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in sellBasket)
            {
                if (slot.IsEmpty)
                {
                    count++;
                }
            }

            return count;
        }
    }

    public int MyTotalSellBasketCount
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in sellBasket)
            {
                count++;
            }

            return count;
        }
    }

    public int MyFullSellBasketCount
    {
        get
        {
            return MyTotalSellBasketCount - MyEmptySellBasketCount;
        }
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach(SlotScript slot in slots)
            {
                if (slot.IsEmpty)
                {
                    count++;
                }
            }

            return count;
        }
    }

    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in slots)
            {
                count++;
            }

            return count;
        }
    }

    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }

    public int MyEmptySlotCount2
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in slots2)
            {
                if (slot.IsEmpty)
                {
                    count++;
                }
            }

            return count;
        }
    }

    public int MyTotalSlotCount2
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in slots2)
            {
                count++;
            }

            return count;
        }
    }

    public int MyFullSlotCount2
    {
        get
        {
            return MyTotalSlotCount2 - MyEmptySlotCount2;
        }
    }

    public int MyEmptySlotCount3
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in slots3)
            {
                if (slot.IsEmpty)
                {
                    count++;
                }
            }

            return count;
        }
    }

    public int MyTotalSlotCount3
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in slots3)
            {
                count++;
            }

            return count;
        }
    }

    public int MyFullSlotCount3
    {
        get
        {
            return MyTotalSlotCount3 - MyEmptySlotCount3;
        }
    }

    public SlotScript FromSlot
    {
        get
        {
            return fromSlot;
        }

        set
        {
            fromSlot = value;

            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

    void Start() 
    {
        currentBagNumber = 1;    
    }

    private void Update()
    {
        Ribi_t.text = Ribi.ToString();

        if (Input.GetKeyDown(KeyCode.J)) 
        {
            for (int i = 0; i < 10; i++)
                AddItem(items[1]);
        }

        switch (currentBagNumber) 
        {
            case 1:
                SwitchToBag1();
                break;
            case 2:
                SwitchToBag2();
                break;
            case 3:
                SwitchToBag3();
                break;
        }

    }

    public bool AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }
        return PlaceInEmpty(item);
    }

    public bool AddToBuyBasket(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack_BuyBasket(item))
            {
                return true;
            }
        }
        return PlaceInEmpty_BuyBasket(item);
    }

    public bool AddToSellBasket(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack_SellBasket(item))
            {
                return true;
            }
        }
        return PlaceInEmpty_SellBasket(item);
    }

    private bool PlaceInEmpty_BuyBasket(Item item)
    {
        foreach (SlotScript slot in buyBasket)
        {
            if (slot.IsEmpty)
            {
                if (slot.AddItem(item))
                {                    
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }
        return false;
    }

    private bool PlaceInEmpty_SellBasket(Item item)
    {
        foreach (SlotScript slot in sellBasket)
        {
            if (slot.IsEmpty)
            {
                if (slot.AddItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }
        return false;
    }

    private bool PlaceInStack_BuyBasket(Item item)
    {
        foreach (SlotScript slot in buyBasket)
        {
            if (slot.StackItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }

        return false;
    }

    private bool PlaceInStack_SellBasket(Item item)
    {
        foreach (SlotScript slot in sellBasket)
        {
            if (slot.StackItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }

        return false;
    }

    private bool PlaceInEmpty(Item item)
    {
            foreach (SlotScript slot in slots)
            {
                if (slot.IsEmpty)
                {
                    if (slot.AddItem(item))
                    {
                        OnItemCountChanged(item);
                        return true;
                    }
                }
            }

            foreach (SlotScript slot in slots2)
            {
                if (slot.IsEmpty)
                {
                    if (slot.AddItem(item))
                    {
                        OnItemCountChanged(item);
                        return true;
                    }
                }
            }

            foreach (SlotScript slot in slots3)
            {
                if (slot.IsEmpty)
                {
                    if (slot.AddItem(item))
                    {
                        OnItemCountChanged(item);
                        return true;
                    }
                }
            }
        Debug.Log("Inventory is full");
        return false;
    }

    private bool PlaceInStack(Item item)
    {
        foreach (SlotScript slot in slots)
        {
            if (slot.StackItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }

        foreach (SlotScript slot in slots2)
        {
            if (slot.StackItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }

        foreach (SlotScript slot in slots3)
        {
            if (slot.StackItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }
        return false;
    }

    public void OpenClose()
    {
        if (canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }
    
    public Stack<IUsable> GetUsables(IUsable type)
    {
        Stack<IUsable> usables = new Stack<IUsable>();


        foreach (SlotScript slot in slots)
        {
            if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
            {
                foreach (Item item in slot.MyItems)
                {
                    usables.Push(item as IUsable);
                }
            }
        }

        foreach (SlotScript slot in slots2)
        {
            if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
            {
                foreach (Item item in slot.MyItems)
                {
                    usables.Push(item as IUsable);
                }
            }
        }

        foreach (SlotScript slot in slots3)
        {
            if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
            {
                foreach (Item item in slot.MyItems)
                {
                    usables.Push(item as IUsable);
                }
            }
        }

        return usables;
    }

    public void OnItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }

    public int GetItemCount(string title)
    {
        int count = 0;

        foreach (SlotScript slot in slots)
        {
            if (!slot.IsEmpty && slot.MyItem.MyTitle == title)
            {
                count += slot.MyCount;
            }
        }

        foreach (SlotScript slot in slots2)
        {
            if (!slot.IsEmpty && slot.MyItem.MyTitle == title)
            {
                count += slot.MyCount;
            }
        }

        foreach (SlotScript slot in slots3)
        {
            if (!slot.IsEmpty && slot.MyItem.MyTitle == title)
            {
                count += slot.MyCount;
            }
        }

        return count;
    }

    public void gainRibi(int ribi) {
        Ribi += ribi;
    }

    public void SwitchToBag1() 
    {
        currentBagNumber = 1;
        bag1btn.image.sprite = activated;
        bag2btn.image.sprite = nonactivated;
        bag3btn.image.sprite = nonactivated;
        bag1.SetActive(true);
        bag2.SetActive(false);
        bag3.SetActive(false);
    }

    public void SwitchToBag2() 
    {
        currentBagNumber = 2;
        bag1btn.image.sprite = nonactivated;
        bag2btn.image.sprite = activated;
        bag3btn.image.sprite = nonactivated;
        bag1.SetActive(false);
        bag2.SetActive(true);
        bag3.SetActive(false);
    }

    public void SwitchToBag3() 
    {
        currentBagNumber = 3;
        bag1btn.image.sprite = nonactivated;
        bag2btn.image.sprite = nonactivated;
        bag3btn.image.sprite = activated;
        bag1.SetActive(false);
        bag2.SetActive(false);
        bag3.SetActive(true);
    }

    public void ClearBuyBasket()
    {
        foreach (SlotScript slot in buyBasket)
        {
            slot.ClearItem();
        }
    }

    public void ClearSellBasket()
    {
        foreach (SlotScript slot in sellBasket)
        {
            slot.ClearItem();
        }
    }

    public SlotScript FindItemSlot(Item item)
    {
        foreach (SlotScript slot in slots3)
        {
            if (slot.MyItem == item)
            {
                return slot;
            }
        }

        foreach (SlotScript slot in slots2)
        {
            if (slot.MyItem == item)
            {
                return slot;
            }
        }

        foreach (SlotScript slot in slots)
        {
            if (slot.MyItem == item)
            {
                return slot;
            }
        }

        return null;
    }

    public Stack<Item> GetItems(string type, int count)
    {
        Stack<Item> items = new Stack<Item>();

        foreach (SlotScript slot in slots)
        {
            if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
            {
                foreach (Item item in slot.MyItems)
                {
                    items.Push(item);

                    if (items.Count == count)
                    {
                        return items;
                    }
                }
            }
        }

        foreach (SlotScript slot in slots2)
        {
            if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
            {
                foreach (Item item in slot.MyItems)
                {
                    items.Push(item);

                    if (items.Count == count)
                    {
                        return items;
                    }
                }
            }
        }

        foreach (SlotScript slot in slots3)
        {
            if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
            {
                foreach (Item item in slot.MyItems)
                {
                    items.Push(item);

                    if (items.Count == count)
                    {
                        return items;
                    }
                }
            }
        }

        return items;
    }
}

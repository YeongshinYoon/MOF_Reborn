using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    public IUsable MyUsable { get; set; }

    [SerializeField]
    private Text stackSize;

    private Stack<IUsable> usables = new Stack<IUsable>();

    private int count;

    public Button MyButton { get; private set; }

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
            return count;
        }
    }

    public Text MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public Stack<IUsable> MyUsables
    {
        get
        {
            return usables;
        }

        set
        {
            if (value.Count > 0)
            {
                MyUsable = value.Peek();
            }
            else
            {
                MyUsable = null;
            }
            usables = value;
        }
    }

    [SerializeField]
    private Image icon;

    // Use this for initialization
    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMovable == null)
        {
            if (MyUsable != null)
            {
                MyUsable.use();
            }
            else if (MyUsables != null && MyUsables.Count > 0)
            {
                MyUsables.Peek().use();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMovable != null && HandScript.MyInstance.MyMovable is IUsable)
            {
                SetUsable(HandScript.MyInstance.MyMovable as IUsable);
            }
        }
    }

    public void SetUsable(IUsable usable)
    {
        if (usable is Item)
        {
            MyUsables = InventoryScript.MyInstance.GetUsables(usable);
            if (InventoryScript.MyInstance.FromSlot != null)
            {
                InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
                InventoryScript.MyInstance.FromSlot = null;
            }
        }
        else
        {
            MyUsables.Clear();
            MyUsable = usable;
        }

        count = MyUsables.Count;
        UpdateVisual(usable as IMovable);
        UIManager.MyInstance.RefreshToolTip(MyUsable as IDescribable);
    }

    public void UpdateVisual(IMovable movable)
    {
        if (HandScript.MyInstance.MyMovable != null)
        {
            HandScript.MyInstance.Drop();
        }

        MyIcon.sprite = movable.MyIcon;
        MyIcon.color = Color.white;

        if (count > 1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
        else if (MyUsable is Spell)
        {
            UIManager.MyInstance.ClearStackCount(this);
        }
    }

    public void UpdateItemCount(Item item)
    {
        if (item is IUsable && MyUsables.Count > 0)
        {
            if (MyUsables.Peek().GetType() == item.GetType())
            {
                MyUsables = InventoryScript.MyInstance.GetUsables(item as IUsable);

                count = MyUsables.Count;

                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IDescribable tmp = null;

        if (MyUsable != null && (MyUsable is IDescribable))
        {
            tmp = (IDescribable)MyUsable;
            //UIManager.MyInstance.ShowTooltip(transform.position);
        }
        else if (MyUsables.Count > 0)
        {
            //UIManager.MyInstance.ShowTooltip(transform.position);
        }
        if (tmp != null)
        {
            UIManager.MyInstance.ShowTooltip(transform.position, tmp);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}

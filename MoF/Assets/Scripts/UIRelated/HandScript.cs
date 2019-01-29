using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour {
    private static HandScript instance;

    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }

            return instance;
        }
    }

    public IMovable MyMovable { get; set; }

    private Image icon;

    [SerializeField]
    private Vector3 offset;

	// Use this for initialization
	void Start () 
    {
        icon = GetComponent<Image>();
	}
	
	// Update is called once per frame
    void Update ()
    {
        icon.transform.localPosition = Input.mousePosition + offset;


        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyMovable != null && (MyMovable as Item).IsRemovable)
        {
            DeleteItem();
        }
	}

    public void TakeMovable(IMovable movable)
    {
        MyMovable = movable;
        icon.sprite = movable.MyIcon;
        icon.color = Color.white;
    }

    public IMovable Put()
    {
        IMovable tmp = MyMovable;

        MyMovable = null;

        icon.color = new Color(0, 0, 0, 0);

        return tmp;
    }

    public void Drop()
    {
        MyMovable = null;
        icon.color = new Color(0, 0, 0, 0);
        InventoryScript.MyInstance.FromSlot = null;
    }

    public void DeleteItem()
    {
        if (MyMovable is Item)
        {
            Item item = (Item)MyMovable;
            if (item.MySlot != null)
            {
                InventoryScript.MyInstance.FromSlot.Clear();
            }
            else if (item.MyEquipmentButton != null)
            {
                item.MyEquipmentButton.DequipArmor();
            }
        }
        
        Drop();

        InventoryScript.MyInstance.FromSlot = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    private ArmorType armoryType;

    private Armor equippedArmor;

    public Armor MyEquippedArmor
    {
        get
        {
            return equippedArmor;
        }
    }

    [SerializeField]
    private Image icon;

    private Sprite sprite;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMovable is Armor)
            {
                Armor tmp = (Armor)HandScript.MyInstance.MyMovable;

                if (tmp.MyArmorType == armoryType)
                {
                    EquipArmor(tmp);
                }

                UIManager.MyInstance.RefreshToolTip(tmp);
            }
            else if (HandScript.MyInstance.MyMovable == null && equippedArmor != null)
            {
                HandScript.MyInstance.TakeMovable(equippedArmor);
                Equipment.MyInstance.MySelectedButton = this;
                icon.color = Color.gray;
            }
        }
    }

    public void EquipArmor(Armor armor)
    {
        armor.Remove();

        if (equippedArmor != null)
        {
            if (equippedArmor != armor)
            {
                armor.MySlot.AddItem(equippedArmor);
            }
            UIManager.MyInstance.RefreshToolTip(equippedArmor);
        }
        else
        {
            UIManager.MyInstance.HideTooltip();
        }

        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        icon.color = Color.white;
        equippedArmor = armor;
        equippedArmor.MyEquipmentButton = this;
        
        if (HandScript.MyInstance.MyMovable == (armor as IMovable))
        {
            HandScript.MyInstance.Drop();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedArmor != null)
        {
            UIManager.MyInstance.ShowTooltip(transform.position, equippedArmor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }

    public void DequipArmor()
    {
        icon.color = Color.white;
        icon.sprite = sprite;

        equippedArmor.MyEquipmentButton = null;
        equippedArmor = null;
    }

    void Start()
    {
        sprite = icon.sprite;
    }
}

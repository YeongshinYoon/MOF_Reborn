using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
    private static Equipment instance;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private EquipmentButton helmet, neckless, ring1, ring2, ring3, weapon, shield, upper, lower, gloves, shoes;

    public static Equipment MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Equipment>();
            }

            return instance;
        }
    }

    public EquipmentButton MySelectedButton { get; set; }

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

    public void EquipArmor(Armor armor)
    {
        switch(armor.MyArmorType)
        {
            case ArmorType.Helmet:
                helmet.EquipArmor(armor);
                break;
            case ArmorType.Neckless:
                neckless.EquipArmor(armor);
                break;
            case ArmorType.Ring:
                ring1.EquipArmor(armor);
                break;
            case ArmorType.Weapon:
                weapon.EquipArmor(armor);
                break;
            case ArmorType.Shield:
                shield.EquipArmor(armor);
                break;
            case ArmorType.Upper:
                upper.EquipArmor(armor);
                break;
            case ArmorType.Lower:
                lower.EquipArmor(armor);
                break;
            case ArmorType.Gloves:
                gloves.EquipArmor(armor);
                break;
            case ArmorType.Shoes:
                shoes.EquipArmor(armor);
                break;
        }
    }
}

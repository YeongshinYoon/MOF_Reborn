using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArmorType {Helmet, Neckless, Ring, Weapon, Shield, Upper, Lower, Gloves, Shoes}

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item {
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private string className;

    [SerializeField]
    private int strength;

    [SerializeField]
    private int vitality;

    [SerializeField]
    private int agility;

    [SerializeField]
    private int intellegence;

    internal ArmorType MyArmorType
    {
        get
        {
            return armorType;
        }
    }


    public override string GetDescription()
    {
        string stats = string.Empty;

        stats += string.Format("\n 직업 : {0}", className);

        if (strength > 0)
        {
            stats += string.Format("\n 공격 + {0}", strength);
        }

        if (vitality > 0)
        {
            stats += string.Format("\n 체력 + {0}", vitality);
        }

        if (agility > 0)
        {
            stats += string.Format("\n 민첩 + {0}", agility);
        }
        
        if (intellegence > 0)
        {
            stats += string.Format("\n 지력 + {0}", intellegence);
        }

        return base.GetDescription() + stats;
    }

    public void Equip()
    {
        Equipment.MyInstance.EquipArmor(this);
    }
}

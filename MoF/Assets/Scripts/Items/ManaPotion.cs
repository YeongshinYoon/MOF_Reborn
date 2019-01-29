using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "마력 회복제", menuName = "Items/물약/마력 회복제", order = 1)]
public class ManaPotion : Item, IUsable
{
    [SerializeField]
    private int mana;

    public void use()
    {
        if (Player.MyInstance.MyMana.MyCurrentValue < Player.MyInstance.MyMana.MyMaxValue)
        {
            Remove();

            Player.MyInstance.GetMana(mana);
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\nUse : Restores {0} mana", mana);
    }
}

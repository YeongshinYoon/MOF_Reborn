using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "체력 회복제", menuName = "Items/물약/체력 회복제", order = 0)]
public class HealthPotion : Item, IUsable {
    [SerializeField]
    private int health;

    public void use()
    {
        if (Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
        {
            Remove();

            Player.MyInstance.GetHealth(health);
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\nUse : Restores {0} health", health);
    }
}

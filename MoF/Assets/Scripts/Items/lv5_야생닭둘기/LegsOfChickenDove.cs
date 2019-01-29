using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "닭둘기 다리", menuName = "Items/몬스터/야생 닭둘기/닭둘기 다리", order = 1)]
public class LegsOfChickenDove : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("날카로운 발톱이 있는 닭둘기의 다리이다.");
    }
}

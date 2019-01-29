using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "신비한 깃털", menuName = "Items/몬스터/야생 닭둘기/신비한 깃털", order = 2)]
public class MysteriousFeather : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n신비한 마력이 담긴 깃털이다.");
    }
}

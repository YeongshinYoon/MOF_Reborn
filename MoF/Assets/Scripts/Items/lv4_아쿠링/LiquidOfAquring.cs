using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "아쿠링 액체", menuName = "Items/몬스터/아쿠링/아쿠링 액체", order = 2)]
public class LiquidOfAquring : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n아쿠링의 일부인 액체이다.");
    }
}

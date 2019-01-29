using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "아쿠링 진물", menuName = "Items/몬스터/아쿠링/아쿠링 진물", order = 0)]
public class DischargeOfAquring : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n아쿠링이 지나다닐 때 생기는 진물이다.");
    }
}

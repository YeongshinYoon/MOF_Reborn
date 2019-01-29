using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "아쿠아 보석", menuName = "Items/몬스터/아쿠링/아쿠아 보석", order = 1)]
public class JewelOfAquring : Item
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n아쿠링의 핵을 이루는 보석이다.");
    }
}

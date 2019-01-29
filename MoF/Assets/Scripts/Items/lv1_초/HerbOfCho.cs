using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "초의 약초", menuName = "Items/몬스터/초/초의 약초", order = 0)]
public class HerbOfCho : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n초의 머리 잎에서 찾아낸 약초");
    }
}

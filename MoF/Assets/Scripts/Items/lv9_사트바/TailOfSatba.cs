using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "사트바 꼬리", menuName = "Items/몬스터/사트바/사트바 꼬리", order = 2)]
public class TailOfSatba : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n사트바의 귀중한 꼬리이다.");
    }
}

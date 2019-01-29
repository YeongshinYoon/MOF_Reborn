using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "사트바 샅바", menuName = "Items/몬스터/사트바/사트바 샅바", order = 1)]
public class SatbaOfSatba : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n사트바의 보물인 샅바이다.");
    }
}

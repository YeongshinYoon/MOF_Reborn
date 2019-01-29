using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "도토리", menuName = "Items/몬스터/사트바/도토리", order = 0)]
public class Acorn : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n사트바의 양식인 도토리이다.");
    }
}

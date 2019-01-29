using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "일개미 다리", menuName = "Items/몬스터/일개미/일개미 다리", order = 1)]
public class LegsOfWorkerAnt : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n일개미의 배에 붙어 있던 다리이다.");
    }
}

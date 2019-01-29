using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "일개미 더듬이", menuName = "Items/몬스터/일개미/일개미 더듬이", order = 0)]
public class FeelersOfWorkerAnt : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n일개미의 머리에 붙어있는 더듬이다.");
    }
}

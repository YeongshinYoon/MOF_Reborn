using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "일개미 배", menuName = "Items/몬스터/일개미/일개미 배", order = 2)]
public class StomachOfWorkerAnt : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n사냥하면서 잘려나온 일개미의 배이다.");
    }
}

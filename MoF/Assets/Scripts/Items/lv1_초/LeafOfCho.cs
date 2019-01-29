using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "초 머리잎", menuName = "Items/몬스터/초/초 머리잎", order = 1)]
public class LeafOfCho : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n초의 머리에서 자라는 머리 잎");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "부드러운 털", menuName = "Items/몬스터/뭉치/부드러운 털", order = 1)]
public class SoftFur : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n뭉치의 부드러운 털이다.\n옷감 재료로도 쓰인다.");
    }
}

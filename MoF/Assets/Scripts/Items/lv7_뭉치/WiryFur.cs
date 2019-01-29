using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "뻣뻣한 털", menuName = "Items/몬스터/뭉치/뻣뻣한 털", order = 2)]
public class WiryFur : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n너무 뻣뻣한 털이다.\n가공해서 사용해야 한다.");
    }
}

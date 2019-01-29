using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "뭉치 털", menuName = "Items/몬스터/뭉치/뭉치 털", order = 0)]
public class FurOfMungChi : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n뭉치의 푸짐한 털이다.");
    }
}

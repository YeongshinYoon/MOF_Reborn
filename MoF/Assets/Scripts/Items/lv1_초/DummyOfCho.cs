using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "초의 쪽쪽이", menuName = "Items/몬스터/초/초의 쪽쪽이", order = 2)]
public class DummyOfCho : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n초가 입에 물고 다니는 쪽쪽이다.");
    }
}

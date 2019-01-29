using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "나뭇잎", menuName = "Items/몬스터/야생 닭둘기/나뭇잎", order = 0)]
public class Leaf : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n닭둘기의 머리에 붙은 나뭇잎이다.");
    }
}

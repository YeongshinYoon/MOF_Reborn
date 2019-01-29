using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "빛나는 반지", menuName = "Items/퀘스트/빛나는 반지", order = 11)]
public class GlowingRing : Item
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n커다랗게 박힌 보석이 유난히 빛나는 반지이다. \n어서 룬페이에게 전달하자.");
    }
}


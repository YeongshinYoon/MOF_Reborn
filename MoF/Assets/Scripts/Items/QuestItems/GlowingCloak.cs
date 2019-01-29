using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "빛나는 망토", menuName = "Items/퀘스트/빛나는 망토", order = 12)]
public class GlowingCloak : Item
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n왠지 모르게 빛나고 있는 망토이다. \n아직 어디에 쓰이는 지 확인되지 않았다.");
    }
}

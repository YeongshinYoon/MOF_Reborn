using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "작은 단검", menuName = "Items/퀘스트/작은 단검", order = 13)]
public class LittleShortSword : Item
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n왠지 모르게 빛나고 있는 망토이다. \n아직 어디에 쓰이는 지 확인되지 않았다.");
    }
}

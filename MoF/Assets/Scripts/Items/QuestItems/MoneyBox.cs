using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "돈상자", menuName = "Items/퀘스트/돈상자", order = 10)]
public class MoneyBox : Item
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n필립이 오스발에게 판매한 무기들의 값이다.\n어서 필립에게 전달하자.");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "초콜릿 상자", menuName = "Items/퀘스트/초콜릿 상자", order = 9)]
public class ChocolateBox : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n제인이 짝사랑하는 페이를 위해 만든 초콜릿이 들어있는 상자이다.\n장인의 거리의 대장간에 있는 페이에게 전달하자.");
    }
}

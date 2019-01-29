using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "약초 주머니", menuName = "Items/퀘스트/약초 주머니", order = 5)]
public class HerbPouch : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n키단이 감정을 의뢰한 약초 주머니이다.\n잡화점에 있는 로시에게 전달하자.");
    }
}

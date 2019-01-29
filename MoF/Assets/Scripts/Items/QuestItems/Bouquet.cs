using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "꽃다발", menuName = "Items/퀘스트/꽃다발", order = 1)]
public class Bouquet : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n제인이 아버지인 버즈를 위해 정성스레 만든 꽃다발이다.\n어서 버즈 선생님께 전달하자.");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "로시의 편지", menuName = "Items/퀘스트/로시의 편지", order = 7)]
public class LetterOfRosi : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n로시가 필립에게 전해달라던 편지이다.\n주점 밑 도둑길드에 있는 필립에게 전달하자.");
    }
}

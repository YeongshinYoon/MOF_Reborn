using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "호크아이의 편지", menuName = "Items/퀘스트/호크아이의 편지", order = 3)]
public class LetterOfHawkeye : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n호크아이가 리오나에게 전해달라던 편지이다.\n잡화점에 있는 리오나에게 전달하자.");
    }
}

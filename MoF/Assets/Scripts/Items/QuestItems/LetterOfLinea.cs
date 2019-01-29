using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "리네아의 편지", menuName = "Items/퀘스트/리네아의 편지", order = 6)]
public class LetterOfLinea : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n리네아가 세리자드에게 전해달라던 편지이다.\n광장에 있는 세리자드에게 전달하자.");
    }
}

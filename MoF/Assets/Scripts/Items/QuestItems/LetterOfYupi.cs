using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "유피의 소개장", menuName = "Items/퀘스트/유피의 소개장", order = 2)]
public class LetterOfYupi : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n유피가 써준 소개장이다.\n어서 하로크 선장님께 전달하자.");
    }
}

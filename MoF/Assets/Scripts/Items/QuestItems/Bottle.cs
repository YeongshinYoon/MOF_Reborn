using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "술병", menuName = "Items/퀘스트/술병", order = 4)]
public class Bottle : Item
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n룬페이가 좋아하는 독주가 담겨있던 빈 술병이다.\n주점에 있는 베르딘에게 전달하자.");
    }
}

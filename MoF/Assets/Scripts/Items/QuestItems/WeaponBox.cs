using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "무기상자", menuName = "Items/퀘스트/무기상자", order = 8)]
public class WeaponBox : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n필립이 몬스터에게서 빼앗은 무기들을 담은 상자이다.\n장인의 거리의 대장간에 있는 오스발에게 전달하자.");
    }
}

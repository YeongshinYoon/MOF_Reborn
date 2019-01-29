using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "입학증", menuName = "Items/퀘스트/입학증", order = 0)]
public class AdmissionCertificate : Item {
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n\n로라가 오랜만에 꼼꼼히 만들었다는 입학증이다.\n어서 크루노 교장선생님께 전달하자.");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAcceptScript : MonoBehaviour 
{
    public Quest MyQuest { get; set; }

    public void Select()
    {
        QuestAccept.MyInstance.ShowQuestInfo(MyQuest, 1);
    }
}

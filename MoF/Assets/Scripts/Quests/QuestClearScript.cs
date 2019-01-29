using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestClearScript : MonoBehaviour 
{
    public Quest MyQuest { get; set; }

    public void Select()
    {
        QuestAccept.MyInstance.ShowQuestInfo(MyQuest, 2);
    }
}

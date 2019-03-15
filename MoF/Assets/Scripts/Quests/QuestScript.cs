using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour {
    [SerializeField]
    private Image highlightedContent;

    public Quest MyQuest { get; set; }

    [SerializeField]
    private Text completion;

    public void Select()
    {
        highlightedContent.color = new Color32(255, 255, 255, 100);
        Questlog.MyInstance.ShowDescription(MyQuest);
    }

    public void DeSelect()
    {
        highlightedContent.color = new Color32(255, 255, 255, 0);
    }

    public void IsComplete()
    {
        if (MyQuest.IsCompletable)
        {
            completion.text = "완료";
            if (!MyQuest.messagePopuped)
            {
                MyQuest.messagePopuped = true;
                MessageFeedManager.MyInstance.WriteMessage(string.Format("{0} 퀘스트 완료 !", MyQuest.MyTitle));
            }
        }
        else if (!MyQuest.IsCompletable)
        {
            MyQuest.messagePopuped = false;
            completion.text = "";
        }
    }
}
